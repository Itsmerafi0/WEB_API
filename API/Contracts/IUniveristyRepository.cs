using API.Models;

namespace API.Contracts
{
    public interface IUniveristyRepository : IGeneralRepository<University>
    {
        IEnumerable<University> GetByName(string name);
    }
}
