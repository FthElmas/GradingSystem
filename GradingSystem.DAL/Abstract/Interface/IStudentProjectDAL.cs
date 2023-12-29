using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DTO.DTOs.StudentProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IStudentProjectDAL
    {
        bool AddStudentProjectWithMark(int projectID, StudentMarkAddDTO studentMark, string description);
        bool UpdateStudentProjectWithMark(StudentProjectDTO student, int mark);
        bool DeleteStudentProjectWithMark(StudentProjectDTO student);
        List<StudentProjectSelectDTO> GetAll();
        StudentProjectDTO GetByID(Guid student, int project);
        List<List<StudentProjectSelectDTO>> GetAllProjectInSelectedWeekOfStudent(List<StudentSelectDTO> students, int selectedWeek);
    }
}
