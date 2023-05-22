using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    public BookingRepository(BookingManagementDbContext context) : base(context) { }

    public IEnumerable<Booking> GetByRoomId(Guid roomId)
    {
        return _context.Set<Booking>().Where(r => r.RoomGuid == roomId);
    }
    public IEnumerable<Booking> GetByEmployeeId(Guid employeeId)
    {
        return _context.Set<Booking>().Where(e => e.EmployeeGuid == employeeId);
    }
}