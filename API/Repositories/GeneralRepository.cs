using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class GeneralRepository<Tentity> : IGeneralRepository<Tentity>where Tentity : class
    {
        protected readonly BookingManagementDbContext _context;
        public GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        /*
         * <summary>
         * Create a new university
         * </summary>
         * <param name="university">University object</param>
         * <returns>University object</returns>
         */
        public Tentity? Create(Tentity tentity)
        {
            try
            {
                typeof(Tentity).GetProperty("CreatedDate")!.SetValue(tentity, DateTime.Now);
                typeof(Tentity).GetProperty("ModifiedDate")!.SetValue(tentity, DateTime.Now);
                _context.Set<Tentity>().Add(tentity);
                _context.SaveChanges();
                return tentity;
            }
            catch
            {
                return null;
            }
        }

        /*
         * <summary>
         * Update a university
         * </summary>
         * <param name="university">University object</param>
         * <returns>true if data updated</returns>
         * <returns>false if data not updated</returns>
         */
        public bool Update(Tentity tentity)
        {
            try
            {
                var guid = (Guid)typeof(Tentity).GetProperty("Guid")!
                                                .GetValue(tentity)!;
                var oldEntity = GetByGuid(guid);
                if(oldEntity == null) {
                    return false;
                }
                var getCreatedDate = typeof(Tentity).GetProperty("CreatedDate")!
                                                    .GetValue(oldEntity)!;
                
                typeof(Tentity).GetProperty("CreatedDate")!
                               .SetValue(tentity, getCreatedDate);
                typeof(Tentity).GetProperty("ModifiedDate")!
                               .SetValue(tentity, DateTime.Now);

                _context.Set<Tentity>().Update(tentity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * <summary>
         * Delete a university
         * </summary>
         * <param name="guid">University guid</param>
         * <returns>true if data deleted</returns>
         * <returns>false if data not deleted</returns>
         */
        public bool Delete(Guid guid)
        {
            try
            {
                var entity = GetByGuid(guid);
                if (entity == null)
                {
                    return false;
                }

                _context.Set<Tentity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * <summary>
         * Get all universities
         * </summary>
         * <returns>List of universities</returns>
         * <returns>Empty list if no data found</returns>
         */
        public IEnumerable<Tentity> GetAll()
        {
            return _context.Set<Tentity>().ToList();
        }

        /*
         * <summary>
         * Get a university by guid
         * </summary>
         * <param name="guid">University guid</param>
         * <returns>University object</returns>
         * <returns>null if no data found</returns>
         */
        public Tentity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<Tentity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;

        }
    }
}
