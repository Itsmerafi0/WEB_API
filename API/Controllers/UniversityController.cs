using API.Contracts;
using API.Models;
using API.ViewModels.Educations;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels;
using API.ViewModels.Others;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniveristyRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<University, UniversityVM> _mapper;
        private readonly IMapper<Education, EducationVM> _educationMapper;
        public UniversityController(IUniveristyRepository universityRepository, IEducationRepository educationRepository, IMapper<University, UniversityVM> mapper, IMapper<Education, EducationVM> educationMapper)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _mapper = mapper;
            _educationMapper = educationMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var universities = _universityRepository.GetAll();
            if(!universities.Any())
            {
                return NotFound(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Not Found Data University"
                });

            }
               var data = universities.Select(_mapper.Map).ToList();

            return Ok(new ResponseVM<List<UniversityVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data University",
                Data = data
            });

        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var university = _universityRepository.GetByGuid(id);
            if(university is null)
            {
                return NotFound(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status404NotFound
                    , Status = HttpStatusCode.OK.ToString(),
                    Message = "Guid Not Found"
                });
            }

            var data = _mapper.Map(university);

            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Found",
                Data = data
            });

        }


        [HttpPost]
        public IActionResult Create(UniversityVM universityVM)
        {
            var universityConverted = _mapper.Map(universityVM);

            var result = _universityRepository.Create(universityConverted);
            if(result is null)
            {
                return BadRequest(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status400BadRequest
                    , Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Create University Failed"
                });
            }
            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create University Success"
            });
        }

        [HttpPut]   
        public IActionResult Update(UniversityVM universityVM)
        {
            var universityConverted = _mapper.Map(universityVM);

            var IsUpdate = _universityRepository.Update(universityConverted);
            if(!IsUpdate)
            {
                return BadRequest(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Update University Failed"
                });
            }
            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update University Success"
            });
        }

        [HttpDelete("{guid}")]
            public IActionResult Delete(Guid guid)
        {
            var isDeleted = _universityRepository.Delete(guid);
            if(!isDeleted)
            {
                return BadRequest(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Delete University Failed"
                });
            }
            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update University Success"
            });
        }
    }

}
