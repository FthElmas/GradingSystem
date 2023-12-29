using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[Customer]")]
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; }

        [Column("CompanyName")]
        public string CompanyName { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
