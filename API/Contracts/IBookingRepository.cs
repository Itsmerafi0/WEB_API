using API.Models;
using API.ViewModels.Bookings;

namespace API.Contracts
{
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
        IEnumerable<Booking> GetByRoomId(Guid roomId);
        IEnumerable<Booking> GetByEmployeeId(Guid employeeId);

        IEnumerable<BookingDurationVM> GetBookingDuration();
        IEnumerable<BookingDetailVM> GetAllBookingDetail();
        BookingDetailVM GetBookingDetailByGuid(Guid guid);
    }
}
