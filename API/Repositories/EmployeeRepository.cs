﻿using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Employees;
using API.ViewModels.Universities;
using System.Text.RegularExpressions;

namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }

        public bool CheckEmailAndPhoneAndNIK(string value)
        {
            return _context.Employees.Any(e => e.Email == value || 
                                            e.PhoneNumber == value || 
                                            e.Nik == value);
        }

        public IEnumerable<MasterEmployeeVM> GetAllMasterEmployee()
        {
            var employees = GetAll();
            var educations = _context.Educations.ToList();
            var universities = _context.Universities.ToList();

            var employeeEducations = new List<MasterEmployeeVM>();

            foreach (var employee in employees)
            {
                var education = educations.FirstOrDefault(e => e.Guid == employee?.Guid);
                var university = universities.FirstOrDefault(u => u.Guid == education?.UniversityGuid);

                if (education != null && university != null)
                {
                    var employeeEducation = new MasterEmployeeVM
                    {
                        Guid = employee.Guid,
                        NIK = employee.Nik,
                        FullName = employee.FirstName + " " + employee.LastName,
                        BirthDate = employee.BirthDate,
                        Gender = employee.Gender.ToString(),
                        HiringDate = employee.HiringDate,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        Major = education.Major,
                        Degree = education.Degree,
                        GPA = education.Gpa,
                        UniversityName = university.Name
                    };

                    employeeEducations.Add(employeeEducation);
                }
            }

            return employeeEducations;
        }


        MasterEmployeeVM? IEmployeeRepository.GetMasterEmployeeByGuid(Guid guid)
        {
            var employee = GetByGuid(guid);
            var educations = _context.Educations.Find(guid);
            var universities = _context.Universities.Find(educations.UniversityGuid);

            var data = new MasterEmployeeVM
            {
                Guid = employee.Guid,
                NIK = employee.Nik,
                FullName = employee.FirstName + " " + employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = educations.Major,
                Degree = educations.Degree,
                GPA = educations.Gpa,
                UniversityName = universities.Name
            };

            return data;
        }

        public Guid? FindGuidByEmail(string email)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
                if (employee == null)
                {
                    return null;
                }
                return employee.Guid;
            }
            catch
            {
                return null;
            }

        }

       public Employee GetEmail(string email)
        {
            var employee = _context.Set<Employee>().FirstOrDefault(e => e.Email == email);

            return employee;
        }
    }

}