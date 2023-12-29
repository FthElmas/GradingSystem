using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[User]")]
    public class User
    {
        [Key]
        public Guid UserID { get; set; }

        [Column("Username")]
        public string Username { get; set; }
        [Column("PasswordHash")]
        public byte[] PasswordHash { get; set; }

        [Column("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
