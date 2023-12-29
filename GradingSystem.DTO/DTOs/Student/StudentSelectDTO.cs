using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Student
{
    public class StudentSelectDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public int CustomerCourseID { get; set; }
        public bool IsEliminated { get; set; }
        public bool IsActive { get; set; }
    }   
}
