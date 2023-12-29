using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[Teacher]")]
    public class Teacher
    {
        [Key]
        public Guid TeacherID { get; set; }

        [Column("TeacherName")]
        public string TeacherName { get; set; }

        [Column("TeacherSurname")]
        public string TeacherSurname { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
