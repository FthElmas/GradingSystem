using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Student
{
    public class StudentAddDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string Email { get; set; }
        public string GraduationState { get; set; }
        public int CustomerCourseID { get; set; }
        public bool IsEliminated { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
