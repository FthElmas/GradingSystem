using GradingSystem.BLL.Services;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.Logic;
using GradingSystem.DTO.DTOs.Complex;
using GradingSystem.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GradingSystem.Controllers
{
    public class StudentpageController : Controller
    {
        IQuizDAL _quiz;
        IStudentReportDAL _report;
        IProjectDAL _project;
        IStudentProjectDAL _studentProject;
        IStudentQuiz _studentQuiz;
        private static int courseID = 0;
        
        public StudentpageController(IQuizDAL dal, IStudentReportDAL reportDAL, IProjectDAL projectDAL, IStudentProjectDAL studentProject, IStudentQuiz studentQuiz)
        {
            _quiz = dal;
            _report = reportDAL;
            _project = projectDAL;
            _studentProject = studentProject;
            _studentQuiz = studentQuiz;
        }
        [HttpGet]
        public ViewResult Index(int courseId)
        {
            ViewData["courseId"] = courseId;
            courseID = courseId;
            var list = _quiz.GetAll().ToList();
            var projectList = _project.GetAll().ToList();
            StudentPageDTO studentPage = new StudentPageDTO()
            {
                QuizList = list.Select(x => new SelectListItem
                {
                    Value = x.QuizID.ToString(),
                    Text = x.QuizName
                }).ToList(),
                ProjectList = projectList.Select(x => new SelectListItem
                {
                    Value = x.ProjectID.ToString(),
                    Text = x.ProjectName
                }).ToList()
            };
            //studentPage.ProjectList = (List<SelectListItem>)_project.GetAll().AsEnumerable();
            return View(studentPage);
        }

        [HttpPost]
        public IActionResult AddQuiz(StudentPageDTO quizz)
        {

                if(_quiz.AddQuiz(new QuizAddDTO()
                {
                    QuizName = quizz.Quiz.QuizName,
                    QuizDescription = quizz.Quiz.QuizDescription,
                    CreatedBy = Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                }))
                {
                    return RedirectToAction("Index", "Studentpage");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

        }


        [HttpPost]
        public IActionResult AddReport([FromBody] StudentPageDTO report)
        {

            if (_report.AddStudentReport(new DAL.DTOs.StudentReport.StudentReportAddDTO()
            {
                ReportName = report.StudentReport.ReportName,
                Report = report.StudentReport.Report,
                CreatedDate = DateTime.Today,
                IsActive = true,
                StudentID = report.StudentReport.StudentID,
                CreatedBy = Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073")
            }))
            {
                return RedirectToAction("Index", "Studentpage");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddProject(StudentPageDTO projectt)
        {
            if(_project.AddProject(new DAL.DTOs.Project.ProjectAddDTO()
            {
                ProjectName = projectt.Project.ProjectName,
                ProjectDescription = projectt.Project.ProjectDescription,
                ProjectStartDate = projectt.Project.ProjectStartDate,
                ProjectEndDate = projectt.Project.ProjectEndDate,
                IsActive = true,
                CreatedDate = DateTime.Today
            }))
            {
                return RedirectToAction("Index", "Studentpage");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddQuizMark([FromBody] StudentPageDTO value)
        {
            if(_studentQuiz.AddStudentQuizWithMark(value.StudentQuiz.QuizID, new DAL.DTOs.StudentMark.StudentMarkAddDTO() {StudentID = value.StudentQuiz.StudentID , Mark = value.StudentMark.Mark, CreatedDate = DateTime.Today, IsActive = true } , value.StudentQuiz.StudentID, value.StudentQuiz.Description))
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult AddProjectMark([FromBody] StudentPageDTO value)
        {
            if(_studentProject.AddStudentProjectWithMark(value.StudentProject.ProjectID, new DAL.DTOs.StudentMark.StudentMarkAddDTO() {StudentID = value.StudentProject.StudentID, Mark = value.StudentMark.Mark, CreatedDate = DateTime.Today, IsActive = true }, value.StudentProject.Description))
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RefreshCard()
        {
            return ViewComponent("Student", new {data = courseID});
        }

    

    }
}
