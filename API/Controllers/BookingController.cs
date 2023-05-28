using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Bookings;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseController<Booking, BookingVM>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        public BookingController(IBookingRepository bookingRepository, IMapper<Booking , BookingVM> mapper) : base (bookingRepository, mapper) 
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }


        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                var data = _bookingRepository.GetAllBookingDetail();

                return Ok(new ResponseVM<List<BookingDetailVM>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Data Di Tampilkan",
                    Data = data.ToList()
                });

            }
            catch
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data tidak bisa ditampilkan"
                });
            }
        }

        [HttpGet("BookingDetailByGuid")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            try
            {
                var booking = _bookingRepository.GetBookingDetailByGuid(guid);
                if (booking is null)
                {

                    return NotFound(new ResponseVM<BookingDetailVM>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Not Found"
                    });
                }

                return Ok(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "GetByGuid Success",
                    Data = booking
                });
            }
            catch
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
        }

        [HttpGet("BookingDuration")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound(new ResponseVM<BookingDurationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(bookingLengths);
        }

    }
}
