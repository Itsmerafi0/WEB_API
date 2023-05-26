using API.Contracts;
using API.Models;
using API.ViewModels;
using API.Repositories;
using API.ViewModels.AccountRoles;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Others;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase

    {   private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
        private readonly IAccountRoleRepository _accountroleRepository;
        public AccountRoleController(IAccountRoleRepository accountroleRepository, IMapper<AccountRole, AccountRoleVM>mapper)
        {
            _mapper = mapper;
            _accountroleRepository = accountroleRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var accountroles = _accountroleRepository.GetAll();
            if (!accountroles.Any())
            {
                return NotFound(new ResponseVM<AccountRoleVM>{
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Not Found"
                });
            }
            var data = accountroles.Select(_mapper.Map).ToList(); 
            return Ok(new ResponseVM<List<AccountRoleVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data AccountRole",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var accountrole = _accountroleRepository.GetByGuid(id);
            if (accountrole is null)
            {
                return NotFound(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Not Found Guid"
                });
            }
            var data = _mapper.Map(accountrole);
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Guid",
                Data = data
            });

        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountRoleVM)
        {
            var accountroleConverted = _mapper.Map(accountRoleVM);
            var result = _accountroleRepository.Create(accountroleConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Create Account Failed"
                });
            }
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Account Success"
            });
        }
        [HttpPut]
        public IActionResult Update(AccountRoleVM accountRoleVM)
        {
            var accountroleConverted = _mapper.Map(accountRoleVM);
            var IsUpdate = _accountroleRepository.Update(accountroleConverted);
            if (!IsUpdate)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Update Account Failed"
                });
            }
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Account Success"
            });
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountroleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Create Account Failed"
                });
            }
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Account Create"
            });
        }
    }

}

