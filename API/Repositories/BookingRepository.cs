using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Bookings;
using System.Reflection.Metadata.Ecma335;

namespace API.Repositories;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    private readonly IRoomRepository _roomRepository;
    public BookingRepository(BookingManagementDbContext context, IRoomRepository roomRepository) : base(context) {
        _roomRepository = roomRepository;
    }

    private int CalculateBookingDuration(DateTime startDate, DateTime endDate)
    {
        int totalDays = 0;
        DateTime currentDate = startDate.Date;

        while (currentDate <= endDate)
        {
            if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                totalDays++;
            }

            currentDate = currentDate.AddDays(1);
        }

        return totalDays;
    }

    public IEnumerable<BookingDurationVM> GetBookingDuration()
    {
        var rooms = _roomRepository.GetAll();

        var bookings = GetAll();

        var bookingduration = bookings.Select(b => new BookingDurationVM
        {
            RoomName = rooms.FirstOrDefault(r => r.Guid == b.RoomGuid)?.Name,
            DurationOfBooking = CalculateBookingDuration(b.StartDate, b.EndDate)
        });
        return bookingduration;
    }

    public BookingDetailVM GetBookingDetailByGuid(Guid guid)
    {
        var booking = GetByGuid(guid);
        var employee = _context.Employees.Find(booking.EmployeeGuid);
        var room = _context.Rooms.Find(booking.RoomGuid);

        var bookingDetail = new BookingDetailVM
        {
            Guid = booking.Guid,
            BookedNIK = employee.Nik,
            Fullname = employee.FirstName + " " + employee.LastName,
            RoomName = room.Name,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks
        };

        return bookingDetail;
    }
    //get all

    public IEnumerable<BookingDetailVM> GetAllBookingDetail()
    {
        var bookings = GetAll();
        var employees = _context.Employees.ToList();
        var rooms = _context.Rooms.ToList();
        var results = new List<BookingDetailVM>();
        var bookingDetail = from b in bookings
                            join e in employees on b.EmployeeGuid equals e.Guid
                            join r in rooms on b.RoomGuid equals r.Guid
                            select new
                            {
                                b.Guid,
                                e.Nik,
                                BookedBy = e.FirstName + " " + e.LastName,
                                r.Name,
                                b.StartDate,
                                b.EndDate,
                                b.Status,
                                b.Remarks
                            };



        foreach (var booking in bookingDetail)
        {
            var result = new BookingDetailVM
            {
                Guid = booking.Guid,
                BookedNIK = booking.Nik,
                Fullname = booking.BookedBy,
                RoomName = booking.Name,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks
            };

            results.Add(result);

        }
        return results;
    }


    public IEnumerable<Booking> GetByRoomId(Guid roomId)
    {
        return _context.Set<Booking>().Where(r => r.RoomGuid == roomId);
    }
    public IEnumerable<Booking> GetByEmployeeId(Guid employeeId)
    {
        return _context.Set<Booking>().Where(e => e.EmployeeGuid == employeeId);
    }


}