using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DTO.DTOs.StudentQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IStudentQuiz
    {
        List<StudentQuizSelectDTO> GetAll();
        bool DeleteStudentQuizWithMark(StudentQuizAddDTO student);
        bool UpdateStudentQuizWithMark(StudentQuizAddDTO student, int mark);
        bool AddStudentQuizWithMark(int quizID, StudentMarkAddDTO studentMark, Guid studentID, string description);
        StudentQuizAddDTO GetByID(Guid student, int quiz);
        List<List<StudentQuizSelectDTO>> GetAllQuizInSelectedWeekOfStudent(List<StudentSelectDTO> students, int selectedWeek);
    }
}
