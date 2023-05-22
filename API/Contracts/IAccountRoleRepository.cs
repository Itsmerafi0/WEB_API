using API.Models;

namespace API.Contracts
{
    public interface IAccountRoleRepository :IGeneralRepository<AccountRole>
    {
        IEnumerable<AccountRole> GetByAccountGuid(Guid accountId);
        IEnumerable<AccountRole> GetByRoleGuid(Guid roleGuid);
    }
}
