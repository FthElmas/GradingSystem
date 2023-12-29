using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Teacher
{
    public class TeacherAddDTO
    {
        public Guid TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
