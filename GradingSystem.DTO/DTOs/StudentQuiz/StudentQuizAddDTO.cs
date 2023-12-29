using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.StudentQuiz
{
    public class StudentQuizAddDTO
    {
        public int QuizID { get; set; }
        public int StudentMarkID { get; set; }
        public Guid StudentID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
