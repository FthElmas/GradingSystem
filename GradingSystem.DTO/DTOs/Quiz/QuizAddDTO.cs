using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Quiz
{
    public class QuizAddDTO
    {
        public string QuizName { get; set; }
        public string QuizDescription { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
