using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class EducationRepository : GeneralRepository<Education>, IEducationRepository
    {
        public EducationRepository(BookingManagementDbContext context) : base(context) { }
        public IEnumerable<Education> GetByUniversityId(Guid universityId)
        {
            return _context.Set<Education>().Where(e => e.UniversityGuid == universityId);
        }
    }
}
