using API.Models;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Others;

namespace API.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        int Register(RegisterVM registerVM);

        LoginVM Login(LoginVM loginVM);
        
        // Kelompok 5
        int UpdateOTP(Guid? employeeId);

        // Kelompok 6
        int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);

        IEnumerable<string> GetRoles(Guid guid);

    }
}
