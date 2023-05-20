using API.Models;

namespace API.Contracts
{
    public interface IController<Tentity>  where Tentity : class
    {
        Tentity Create(Tentity tentity);
        bool Update(Tentity tentity);
        bool Delete(Guid guid);

        IEnumerable<Tentity> GetAll();

        Tentity GetByGuid(Guid guid);
    }
}
