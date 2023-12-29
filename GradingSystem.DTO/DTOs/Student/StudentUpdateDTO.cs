using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Student
{
    public class StudentUpdateDTO
    {
        [Key]
        public Guid StudentID { get; set; }
        [Column("StudentName")]
        public string StudentName { get; set; }
        [Column("StudentSurname")]
        public string StudentSurname { get; set; }
        [Column("IsEliminated")]
        public bool IsEliminated { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
