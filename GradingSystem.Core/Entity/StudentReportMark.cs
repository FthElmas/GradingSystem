using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[StudentReportMark]")]
    public class StudentReportMark
    {
        [ForeignKey("StudentMark")]
        public int StudentMarkID { get; set; }
        [ForeignKey("StudentReport")]
        public int StudentReportID { get; set; }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
