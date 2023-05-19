using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("otp")]
        public int OTP { get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }

        //Cardinalitas dengan employee
        public Employee? Employee { get; set; }

        //Cardinalitas One To Many dengan AccountRole
        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
