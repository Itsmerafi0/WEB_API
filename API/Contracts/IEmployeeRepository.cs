using API.Models;
using API.ViewModels.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        int CreateWithValidate(Employee employee);
        Guid? FindGuidByEmail(string email);
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);
    }
}
