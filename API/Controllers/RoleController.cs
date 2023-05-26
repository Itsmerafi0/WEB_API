using API.Contracts;
using API.Models;
using API.ViewModels.AccountRoles;
using API.ViewModels.Roles;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Others;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<Role,RoleVM> _mapper;
        private readonly IMapper<AccountRole, AccountRoleVM> _accountrolemapper;
        public RoleController(IRoleRepository roleRepository, IMapper<Role,RoleVM>mapper, IAccountRoleRepository accountroleRepository, IMapper<AccountRole,AccountRoleVM>accountrolemapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _accountRoleRepository = accountroleRepository;
            _accountrolemapper = accountrolemapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = roles.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<RoleVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Role",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var role = _roleRepository.GetByGuid(id);
            if (role is null)
            {
                return NotFound(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status404NotFound
                    , Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = _mapper.Map(role);
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Guid Success",
                Data = data
            });

        }

        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            var roleConverted = _mapper.Map(roleVM);
            var result = _roleRepository.Create(roleConverted);
            if(result is null)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message ="Create Role Failed"
                });
            }
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Role Success"
            });
        }

        [HttpPut]
        public IActionResult Update(RoleVM roleVM)
        {
            var roleConverted = _mapper.Map(roleVM);
            var IsUpdate = _roleRepository.Update(roleConverted);
            if (!IsUpdate)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Update Role Failed"
                });
            }
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Role Success"
            });
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Delete Role Failed"
                });
            }
            return Ok(new ResponseVM<RoleVM>{
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Role Success"
            });
        }
    }

}

