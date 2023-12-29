using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DTO.DTOs.Complex
{
    public class StudentReportPage
    {
        public List<StudentSelectDTO> Student { get; set; }
        public List<List<StudentReportSelectDTO>> StudentReport { get; set; }
        public List<List<StudentReportSelectDTO>> WeekReport { get; set; }
    }
}
