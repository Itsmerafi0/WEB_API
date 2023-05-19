using API.Models;

namespace API.Contracts
{
    public interface IAccountRoleRepository
    {
        AccountRole Create(AccountRole accountRole);
        bool Update(AccountRole accountRole);
        bool Delete(Guid guid);
        IEnumerable<AccountRole> GetAll();
        AccountRole? GetByGuid(Guid guid);

    }
}
