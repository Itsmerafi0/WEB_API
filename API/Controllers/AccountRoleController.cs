using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IController<AccountRole> _accountroleRepository;
        public AccountRoleController(IController<AccountRole> accountroleRepository)
        {
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
            return Ok(accountroles);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var accountrole = _accountroleRepository.GetByGuid(id);
            if (accountrole is null)
            {
                return NotFound();
            }

            return Ok(accountrole);

        }

        [HttpPost]
        public IActionResult Create(AccountRole accountrole)
        {
            var result = _accountroleRepository.Create(accountrole);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update(AccountRole accountrole)
        {
            var IsUpdate = _accountroleRepository.Update(accountrole);
            if (IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountroleRepository.Delete(guid);
            if (isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }

}

