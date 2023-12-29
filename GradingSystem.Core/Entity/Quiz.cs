using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[Quiz]")]
    public class Quiz
    {
        [Key]
        public int QuizID { get; set; }

        [Column("QuizName")]
        public string QuizName { get; set; }

        [Column("QuizDescription")]
        public string QuizDescription { get; set; }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
