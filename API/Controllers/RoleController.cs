using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound();
            }
            return Ok(roles);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var role = _roleRepository.GetByGuid(id);
            if (role is null)
            {
                return NotFound();
            }

            return Ok(role);

        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            var result = _roleRepository.Create(role);
            if(result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Role role)
        {
            var IsUpdate = _roleRepository.Update(role);
            if (IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }

}

