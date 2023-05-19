using API.Contexts;
using API.Contracts;
using API.Models;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingManagementDbContext _context;
        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public AccountRole Create(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Add(accountRole);
                _context.SaveChanges();
                return accountRole;
            }
            catch
            {
                return new AccountRole();
            }
        }

        public bool Update(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Update(accountRole);
                _context.SaveChanges();
                return true;
            }
            catch
            { return false; }

        }

        public bool Delete(Guid guid)
        {
            try
            {
                var accountrole = GetByGuid(guid);
                if (accountrole == null) {
                    return false;

                }
                _context.Set<AccountRole>().Remove(accountrole);
                _context.SaveChanges();
                return true;
                }
            catch 
            { 
                return false; 
            
            }
        }
        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Find(guid);
        }
    }
}

