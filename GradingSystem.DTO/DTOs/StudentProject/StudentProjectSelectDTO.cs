using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DTO.DTOs.StudentProject
{
    public class StudentProjectSelectDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int StudentMarkID { get; set; }
        public int Mark { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int WeekCount { get; set; }
    }
}
