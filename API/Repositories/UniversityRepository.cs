using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniveristyRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }
        public IEnumerable<University> GetByName(string name)
        {
            return _context.Set<University>().Where(u => u.Name.Contains(name));
        }
    }
}
