using API.Models;

namespace API.Contracts
{
    public interface IEducationRepository : IGeneralRepository<Education>
    {
        IEnumerable<Education> GetByUniversityId(Guid universityId);
    }
}
