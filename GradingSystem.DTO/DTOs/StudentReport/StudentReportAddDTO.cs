using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.StudentReport
{
    public class StudentReportAddDTO
    {
        public string ReportName { get; set; }
        public string Report { get; set; }
        public Guid StudentID { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedWeek { get; set; }
        public bool IsActive { get; set; }
    }
}
