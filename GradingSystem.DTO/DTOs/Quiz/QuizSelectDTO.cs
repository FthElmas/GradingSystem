using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Quiz
{
    public class QuizSelectDTO
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
        public string QuizDescription { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
