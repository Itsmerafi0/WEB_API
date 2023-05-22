using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.AccountRoles;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound();
            }
            var data = accountroles.Select(_mapper.Map).ToList(); 
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var accountrole = _accountroleRepository.GetByGuid(id);
            if (accountrole is null)
            {
                return NotFound();
            }
            var data = _mapper.Map(accountrole);
            return Ok(accountrole);

        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountRoleVM)
        {
            var accountroleConverted = _mapper.Map(accountRoleVM);
            var result = _accountroleRepository.Create(accountroleConverted);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update(AccountRoleVM accountRoleVM)
        {
            var accountroleConverted = _mapper.Map(accountRoleVM);
            var IsUpdate = _accountroleRepository.Update(accountroleConverted);
            if (!IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountroleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }

}

