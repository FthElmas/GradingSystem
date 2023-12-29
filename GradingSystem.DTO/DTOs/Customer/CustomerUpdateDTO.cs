using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Customer
{
    public class CustomerUpdateDTO
    {
        [Key]
        public Guid CustomerID { get; set; }
        [Column("CompanyName")]
        public string CompanyName { get; set; }
        public string Email { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
