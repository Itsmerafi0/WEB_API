using API.Contracts;
using API.Models;
using API.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMapper<Account, AccountVM> _mapper;
    private readonly IAccountRepository _accountRepository;
    public AccountController(IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return NotFound();
        }
        var data = accounts.Select(_mapper.Map).ToList();
        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(account);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);
        var result = _accountRepository.Create(accountConverted);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);
        var isUpdated = _accountRepository.Update(accountConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}