using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Quiz
{
    public class QuizUpdateDTO
    {
        [Key]
        public int QuizID { get; set; }
        [Column("QuizName")]
        public string QuizName { get; set; }
        [Column("QuizDescription")]
        public string QuizDescription { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
