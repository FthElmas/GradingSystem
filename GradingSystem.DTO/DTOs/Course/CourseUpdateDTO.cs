using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Course
{
    public class CourseUpdateDTO
    {
        [Key]
        public int CourseID { get; set; }
        [Column("CourseName")]
        public string CourseName { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
