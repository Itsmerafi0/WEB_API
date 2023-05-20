using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IController<University> _universityRepository;
        public UniversityController(IController<University> universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var universities = _universityRepository.GetAll();
            if(!universities.Any())
            {
                return NotFound();
            }
            return Ok(universities);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var university = _universityRepository.GetByGuid(id);
            if(university is null)
            {
                return NotFound();
            }

            return Ok(university);

        }
        [HttpPost]
        public IActionResult Create(University university)
        {
            var result = _universityRepository.Create(university);
            if(result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]   
        public IActionResult Update(University university)
        {
            var IsUpdate = _universityRepository.Update(university);
            if(IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
            public IActionResult Delete(Guid guid)
        {
            var isDeleted = _universityRepository.Delete(guid);
            if(isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }

}
