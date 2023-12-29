using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IStudentReportDAL
    {
        public bool AddStudentReport(StudentReportAddDTO studentReport);
        StudentReportUpdateDTO GetById(int ID);
        bool AddReportWithMark(StudentMarkAddDTO studentMark,int selectedWeek ,Guid StudentID);
        bool DeleteStudentReport(StudentReportUpdateDTO student);
        bool UpdateStudentReport(StudentReportUpdateDTO student);
        List<StudentReportSelectDTO> GetAllReportInAllWeeks();
        List<StudentReportSelectDTO> GetAllReportInSelectedWeek(int selectedWeek);
        List<StudentReportSelectDTO> GetAllReportInSelectedWeekOfStudent(int selectedWeek, Guid studentID);
        List<StudentReportSelectDTO> GetAllReportInTodayOfStudent(Guid studentID);
        List<List<StudentReportSelectDTO>> GetAllReportInThisWeekOfStudent(List<StudentSelectDTO> students);
        List<List<StudentReportSelectDTO>> GetAllReportInTodayOfStudent(List<StudentSelectDTO> students);
        List<List<StudentReportSelectDTO>> GetAllReportInSelectedWeekOfStudent(List<StudentSelectDTO> students, int selectedWeek);
    }
}
