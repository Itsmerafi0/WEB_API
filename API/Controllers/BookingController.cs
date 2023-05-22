using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        public BookingController(IBookingRepository bookingRepository, IMapper<Booking , BookingVM> mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var bookings = _bookingRepository.GetAll();
            if(!bookings.Any())
            {
                return NotFound();
            }
            var data = bookings.Select(_mapper.Map).ToList();
            return Ok(data);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var booking = _bookingRepository.GetByGuid(id);
            if (booking is null)
            {
                return NotFound();
            }
            var data = _mapper.Map(booking);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var bookingConverted = _mapper.Map(bookingVM);
            var result = _bookingRepository.Create(bookingConverted);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var bookingConverted = _mapper.Map(bookingVM);

            var IsUpdate = _bookingRepository.Update(bookingConverted);
            if (!IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
