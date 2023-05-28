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
    public class RoleController : BaseController<Role, RoleVM>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role,RoleVM> _mapper;
        public RoleController(IRoleRepository roleRepository, IMapper<Role,RoleVM>mapper, IAccountRoleRepository accountroleRepository, IMapper<AccountRole,AccountRoleVM>accountrolemapper) : base (roleRepository, mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
          
        }

    }

}

