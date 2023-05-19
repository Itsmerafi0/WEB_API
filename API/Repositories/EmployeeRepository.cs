using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingManagementDbContext _context;
        public EmployeeRepository(BookingManagementDbContext context)
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
        public Employee Create(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch
            {
                return new Employee();
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
        public bool Update(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Update(employee);
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
                var employee = GetByGuid(guid);
                if (employee == null)
                {
                    return false;
                }

                _context.Set<Employee>().Remove(employee);
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
        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }

        /*
         * <summary>
         * Get a university by guid
         * </summary>
         * <param name="guid">University guid</param>
         * <returns>University object</returns>
         * <returns>null if no data found</returns>
         */

        public Employee? GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find(guid);
        }
    }
}

