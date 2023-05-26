using API.Models;
using API.ViewModels.AccountRoles;

namespace API.ViewModels.Roles
{
    public class RoleAccountRoleVM
    {
        public Guid? Guid { get; set; }

        public string name { get; set; }

        public IEnumerable<AccountRoleVM> AccountRoles { get; set; }
    }
}
