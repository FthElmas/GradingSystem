using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DTO.DTOs.StudentQuiz
{
    public class StudentQuizSelectDTO
    {
        public int QuizID { get; set; }
        public int StudentMarkID { get; set; }
        public int Mark { get; set; }
        public string QuizName { get; set; }
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
