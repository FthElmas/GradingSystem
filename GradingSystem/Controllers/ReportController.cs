using GradingSystem.BLL.Common;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DTO.DTOs.Complex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace GradingSystem.Controllers
{
    public class ReportController : Controller
    {
        IStudentProjectDAL _projectDAL;
        IStudentQuiz _quizDAL;
        IStudentReportDAL _reportDAL;
        IStudentDAL _student;
        ICustomerCourse _customerCourse;
        private List<StudentSelectDTO> students;
        private static int courseID;
        public ReportController(IStudentProjectDAL projectDAL, IStudentQuiz quizDAL, IStudentReportDAL reportDAL, IStudentDAL student, ICustomerCourse customerCourse)
        {
            _projectDAL = projectDAL;
            _quizDAL = quizDAL;
            _reportDAL = reportDAL;
            _student = student;
            _customerCourse = customerCourse;
            students = new List<StudentSelectDTO>();
        }
        public IActionResult Index(int courseId)
        {
            courseID = courseId;
            var data = _customerCourse.GetTotalWeek(courseId);
            List<SelectListItem> list = data.Select(a => new SelectListItem
            {
                Value = a.ToString(),
                Text = a.ToString()
            }).ToList();


            return View(list);
        }

        
        public IActionResult GetReport(int selectedWeek)
        {
            students = _student.GetAllStudent(Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"), courseID);
            MainReportPage reportPage = new MainReportPage
            {
                Student = students,
                StudentReport = _reportDAL.GetAllReportInSelectedWeekOfStudent(students, selectedWeek),
                StudentProject = _projectDAL.GetAllProjectInSelectedWeekOfStudent(students, selectedWeek),
                StudentQuiz = _quizDAL.GetAllQuizInSelectedWeekOfStudent(students, selectedWeek)
            };
            MainReportPageToExcel.ConvertToExcelAndSendEmail(reportPage);
            return View(reportPage);
        }

       

    }
}
