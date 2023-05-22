using API.Contracts;
using API.Models;
using API.Utility;
using API.ViewModels.Educations;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("WithEducation")]
        public IActionResult GetAllWithEducation()
        { 
        var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound();
            }
            var results = new List<UniversityEducationVM>();
            foreach (var university in universities)
            {
                var education = _educationRepository.GetByUniversityId(university.Guid);
                var educationMapped = education.Select(_educationMapper.Map);

                var result = new UniversityEducationVM
                {
                    Guid = university.Guid,
                    Code = university.Code,
                    Name = university.Name,
                    Educations = educationMapped
                };
                results.Add(result);
            }
            return Ok(results);
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var universities = _universityRepository.GetAll();
            if(!universities.Any())
            {
                return NotFound();

            }
               var data = universities.Select(_mapper.Map).ToList();

            return Ok(data);

        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var university = _universityRepository.GetByGuid(id);
            if(university is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(university);

            return Ok(data);

        }
        [HttpPost]
        public IActionResult Create(UniversityVM universityVM)
        {
            var universityConverted = _mapper.Map(universityVM);

            var result = _universityRepository.Create(universityConverted);
            if(result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut]   
        public IActionResult Update(UniversityVM universityVM)
        {
            var universityConverted = _mapper.Map(universityVM);

            var IsUpdate = _universityRepository.Update(universityConverted);
            if(!IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{guid}")]
            public IActionResult Delete(Guid guid)
        {
            var isDeleted = _universityRepository.Delete(guid);
            if(!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }

}
