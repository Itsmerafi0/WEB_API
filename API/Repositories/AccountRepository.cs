using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BookingManagementDbContext _context;
        public AccountRepository(BookingManagementDbContext context)
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
        public Account Create(Account account)
        {
            try
            {
                _context.Set<Account>().Add(account);
                _context.SaveChanges();
                return account;
            }
            catch
            {
                return new Account();
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
        public bool Update(Account account)
        {
            try
            {
                _context.Set<Account>().Update(account);
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
                var account = GetByGuid(guid);
                if (account == null)
                {
                    return false;
                }

                _context.Set<Account>().Remove(account);
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
        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().ToList();
        }

        /*
         * <summary>
         * Get a university by guid
         * </summary>
         * <param name="guid">University guid</param>
         * <returns>University object</returns>
         * <returns>null if no data found</returns>
         */
        public Account? GetByGuid(Guid guid)
        {
            return _context.Set<Account>().Find(guid);
        }
    }
}
