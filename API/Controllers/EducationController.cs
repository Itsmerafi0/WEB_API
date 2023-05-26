using API.Contracts;
using API.Models;
using API.ViewModels;
using API.ViewModels.Educations;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _mapper;
        public EducationController(IEducationRepository educationRepository, IMapper<Education, EducationVM> mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                }) ;
            }
            var resultConverted = educations.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<EducationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Education",
                Data = resultConverted
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var education = _educationRepository.GetByGuid(id);
            if (education is null)
            {
                return NotFound(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = _mapper.Map(education);
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Education",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(EducationVM educationVM)
        {
            var educationConverted = _mapper.Map(educationVM);

            var result = _educationRepository.Create(educationConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest
                    , Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Create Education"
                });
            }
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Create Education"
            });
        }

        [HttpPut]
        public IActionResult Update(EducationVM educationVM)
        {
            var educationConverted = _mapper.Map(educationVM);
            var IsUpdate = _educationRepository.Update(educationConverted);
            if (!IsUpdate)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest
                    , Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Update Education"
                });
            }
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Update Education"
            });
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _educationRepository.Delete(guid);
            if(!isDeleted)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest
                    , Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Delete Education"
                });
            }
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Delete Education"
            });
        }
        
    }
}


