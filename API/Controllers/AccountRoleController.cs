using API.Contracts;
using API.Models;
using API.ViewModels;
using API.Repositories;
using API.ViewModels.AccountRoles;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Others;
using System.Net;
using API.ViewModels.Accounts;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : BaseController<AccountRole, AccountRoleVM>

    {   private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
        private readonly IAccountRoleRepository _accountroleRepository;
        public AccountRoleController(IAccountRoleRepository accountroleRepository, IMapper<AccountRole, AccountRoleVM>mapper) : base(accountroleRepository,mapper)
        {
            _mapper = mapper;
            _accountroleRepository = accountroleRepository;
        }
       
    }

}

