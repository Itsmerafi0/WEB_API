using API.Models;
using API.ViewModels.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        Guid? FindGuidByEmail(string email);
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

        bool CheckEmailAndPhoneAndNIK(string value);
    
        Employee GetEmail(string email);

    }
}
