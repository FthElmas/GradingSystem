using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[StudentReport]")]
    public class StudentReport
    {
        [Key]
        public int StudentReportID { get; set; }
        [Column("ReportName")]
        public string ReportName { get; set; }
        [Column("Report")]
        public string Report { get; set; }
        [ForeignKey("Student")]
        public Guid StudentID { get; set; }
        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [Column("CreatedWeek")]
        public int CreatedWeek { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }


    }
}
