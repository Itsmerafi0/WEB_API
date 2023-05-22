using API.Models;

namespace API.Contracts
{
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
        IEnumerable<Booking> GetByRoomId(Guid roomId);
        IEnumerable<Booking> GetByEmployeeId(Guid employeeId);
    }
}
