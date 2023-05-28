using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Rooms;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Others;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseController<Room, RoomVM>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper<Room, RoomVM> _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper) : base (roomRepository, mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

      
        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomUsedVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found Used Room"
                });
            }

            return Ok(new ResponseVM<IEnumerable<RoomUsedVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message ="Found Used Room",
                Data = room
            });
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound(new ResponseVM<MasterRoomVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found By Date"
                });
            }

            return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found By Date",
                Data = room
            });
        }

        [HttpGet("AvailableRoom")]
        public IActionResult GetAvailableRoom()
        {
            try
            {
                var room = _roomRepository.GetAvailableRoom();
                if (room is null)
                {
                    return NotFound(new ResponseVM<RoomBookedTodayVM>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "AvailableRoom Not Found"
                    });
                }

                return Ok(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "AvailableRoom Found",
                    Data = room
                });
            }
            catch
            {
                return Ok(new ResponseVM<RoomBookedTodayVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Terjadi Error"
                });
            }
        }

    }
}
