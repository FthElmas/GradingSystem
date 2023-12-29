using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.StudentReport
{
    public class StudentReportUpdateDTO
    {
        [Key]
        public int StudentReportID { get; set; }
        [Column("ReportName")]
        public string ReportName { get; set; }
        [Column("Report")]
        public string Report { get; set; }
        [Column("StudentID")]
        public Guid StudentID { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
