using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DTO.FilterDTO
{
    public class ErrorDTO
    {
        public int StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
