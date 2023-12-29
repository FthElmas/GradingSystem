using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.StudentProject
{
    public class StudentProjectDTO
    {
        public Guid StudentID { get; set; }
        public int ProjectID { get; set; }
        public int StudentMarkID { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
