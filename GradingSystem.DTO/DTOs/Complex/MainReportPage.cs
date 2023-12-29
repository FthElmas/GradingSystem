using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DTO.DTOs.StudentProject;
using GradingSystem.DTO.DTOs.StudentQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DTO.DTOs.Complex
{
    public class MainReportPage
    {
        public List<List<StudentReportSelectDTO>> StudentReport { get; set; }
        public List<StudentSelectDTO> Student { get; set; }
        public List<List<StudentQuizSelectDTO>> StudentQuiz { get; set; }
        public List<List<StudentProjectSelectDTO>> StudentProject { get; set; }
    }
}
