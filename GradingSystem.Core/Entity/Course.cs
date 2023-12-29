using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[Course]")]
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Column("CourseName")]
        public string CourseName { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }
    }
}
