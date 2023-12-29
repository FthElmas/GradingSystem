using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Customer
{
    public class CustomerAddDTO
    {
        public Guid CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
