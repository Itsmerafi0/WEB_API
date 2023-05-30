using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Accounts;
using API.Utility;
using System.Linq;
using API.ViewModels.Employees;
using API.ViewModels.Others;

namespace API.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{

    public AccountRepository(
        BookingManagementDbContext context,
        IUniveristyRepository universityRepository,
        IEmployeeRepository employeeRepository,
        IEducationRepository educationRepository
    ) : base(context)
    {
        _universityRepository = universityRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;

    }

    private readonly IUniveristyRepository _universityRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;


    // coba

    public int Register(RegisterVM registerVM)
    {
        try
        {
            var university = new University
            {
                Code = registerVM.UniversitasCode,
                Name = registerVM.UniversityName

            };
            _universityRepository.Create(university);

            var employee = new Employee
            { 
                Nik = GenerateNIK(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            var result = _employeeRepository.Create(employee);

            var education = new Education
            {
                Guid = employee.Guid,
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.Gpa,
                UniversityGuid = university.Guid
            };
            _educationRepository.Create(education);

            var account = new Account
            {
                Guid = employee.Guid,
                Password = Hashing.HashPassword(registerVM.Password),
                IsDeleted = false,
                IsUsed = true,
                OTP = 0
            };

            Create(account);

            var accountRole = new AccountRole
            {
                RoleGuid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                AccountGuid = employee.Guid
            };

                _context.AccountRoles.Add(accountRole);
                _context.SaveChanges();
        
            return 3;

        }
        catch
        {
            return 0;
        }

    }
    private string GenerateNIK()
    {
        var lastNik = _employeeRepository.GetAll().OrderByDescending(e => int.Parse(e.Nik)).FirstOrDefault();

        if (lastNik != null)
        {
            int lastNikNumber;
            if (int.TryParse(lastNik.Nik, out lastNikNumber))
            {
                return (lastNikNumber + 1).ToString();
            }
        }

        return "100000";
    }

    public LoginVM Login(LoginVM loginVM)
    {
       

        var account = GetAll();
        var employee = _context.Employees.ToList();

 
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new LoginVM
                    {
                        Email = emp.Email,
                        Password = acc.Password
                    };
        var data = query.FirstOrDefault();

            if(data != null && Hashing.ValidatePassword(loginVM.Password, data.Password))
            {
            loginVM.Password = data.Password;
            }
        return data;
    }


    public int UpdateOTP(Guid? employeeId)
    {
        var account = new Account();
        account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        Random rnd = new Random();
        var getOTP = rnd.Next(100000, 999999);
        account.OTP = getOTP;

        account.ExpiredTime = DateTime.Now.AddMinutes(5);
        account.IsUsed = false;
        try
        {
            var check = Update(account);

            if(!check)
            {
                return 0;
            }
            return getOTP;
        }
        catch
        {
            return 0;
        }
    }
    public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM)
    {
        var account = new Account();
        account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        if (account == null || account.OTP != changePasswordVM.OTP)
        {
            return 2;
        }
        // Cek apakah OTP sudah digunakan
        if (account.IsUsed)
        {
            return 3;
        }
        // Cek apakah OTP sudah expired
        if (account.ExpiredTime < DateTime.Now)
        {
            return 4;
        }
        // Cek apakah NewPassword dan ConfirmPassword sesuai
        if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
        {
            return 5;
        }
        // Update password
        account.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
        account.IsUsed = true;
        try
        {
            var updatePassword = Update(account);
            if (!updatePassword)
            {
                return 0;
            }
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    public IEnumerable<string> GetRoles(Guid Guid)
    {
        var getAccount = GetByGuid(Guid);
        if (getAccount == null) return Enumerable.Empty<string>();
        var getAccountRoles = from accountRoles in _context.AccountRoles
                              join roles in _context.Roles on accountRoles.RoleGuid equals roles.Guid
                              where accountRoles.AccountGuid == Guid
                              select roles.Name;

        return getAccountRoles.ToList();

    }

}




