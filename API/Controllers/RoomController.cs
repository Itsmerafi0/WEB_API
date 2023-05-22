using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper<Room, RoomVM> _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if(!rooms.Any())
            {
                return NotFound();
            }
            var data = rooms.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var room = _roomRepository.GetByGuid(id);
            if (room is null)
            {
                return NotFound();
            }
            var data = _mapper.Map(room);

            return Ok(data);

        }

        [HttpPost]
        public IActionResult Create(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var result = _roomRepository.Create(roomConverted);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var IsUpdate = _roomRepository.Update(roomConverted);
            if (!IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
