using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.CustomerCourse
{
    public class CustomerCourseAddDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CustomerID { get; set; }
        public int CourseID { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
    }
}
