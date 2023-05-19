using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound();
            }
            return Ok(educations);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var education = _educationRepository.GetByGuid(id);
            if (education is null)
            {
                return NotFound();
            }
            return Ok(education);
        }

        [HttpPost]
        public IActionResult Create(Education education)
        {
            var result = _educationRepository.Create(education);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update(Education education)
        {
            var IsUpdate = _educationRepository.Update(education);
            if (IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _educationRepository.Delete(guid);
            if(isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
        
    }
}


