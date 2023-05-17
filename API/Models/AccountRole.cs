namespace API.Models
{
    public class AccountRole
    {
        public Guid Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModefiedDate { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

    }
}
