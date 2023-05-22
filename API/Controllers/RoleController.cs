using API.Contracts;
using API.Models;
using API.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role,RoleVM> _mapper;
        public RoleController(IRoleRepository roleRepository, IMapper<Role,RoleVM>mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound();
            }
            var data = roles.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var role = _roleRepository.GetByGuid(id);
            if (role is null)
            {
                return NotFound();
            }
            var data = _mapper.Map(role);
            return Ok(data);

        }

        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            var roleConverted = _mapper.Map(roleVM);
            var result = _roleRepository.Create(roleConverted);
            if(result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(RoleVM roleVM)
        {
            var roleConverted = _mapper.Map(roleVM);
            var IsUpdate = _roleRepository.Update(roleConverted);
            if (!IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }

}

