using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.CustomerCourse
{
    public class CustomerCourseUpdateDTO
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
        [Column("CourseStartDate")]
        public DateTime CourseStartDate { get; set; }
        [Column("CourseEndDate")]
        public DateTime CourseEndDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
