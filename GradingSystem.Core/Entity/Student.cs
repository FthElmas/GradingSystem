using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[Student]")]
    public class Student
    {
        [Key]
        public Guid StudentID { get; set; }

        [Column("StudentName")]
        public string StudentName { get; set; }

        [Column("StudentSurname")]
        public string StudentSurname { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("GraduationState")]
        public string GraduationState { get; set; }

        [ForeignKey("CustomerCourse")]
        public int CustomerCourseID { get; set; }

        [Column("IsEliminated")]
        public bool? IsEliminated { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("CreatedBy")]
        public Guid? CreatedBy { get; set; }

        [Column("IsActive")]
        public bool? IsActive { get; set; }

        [NotMapped]
        public CustomerCourse CustomerCourse { get; set; }
    }
}
