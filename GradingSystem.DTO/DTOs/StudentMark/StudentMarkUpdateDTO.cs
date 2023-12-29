using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.StudentMark
{
    public class StudentMarkUpdateDTO
    {
        [Key]
        public int StudentMarkID { get; set; }
        [Column("Mark")]
        public int Mark { get; set; }
        [Column("StudentID")]
        public Guid StudentID { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
