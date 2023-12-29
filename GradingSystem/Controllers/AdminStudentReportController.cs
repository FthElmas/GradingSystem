using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DTO.DTOs.Complex;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.Controllers
{
    public class AdminStudentReportController : Controller
    {
        IStudentDAL _studentDAL;
        IStudentReportDAL _reportDAL;
        public AdminStudentReportController(IStudentDAL studentDAL, IStudentReportDAL reportDAL)
        {
            _studentDAL = studentDAL;
            _reportDAL = reportDAL;
        }
        public IActionResult Index(int courseId)
        {
            AdminReportPage data = new AdminReportPage
            {
                Student = _studentDAL.GetAllStudent(Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"), courseId),
                StudentReport = _reportDAL.GetAllReportInSelectedWeekOfStudent(_studentDAL.GetAllStudent(Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"), courseId), 14)
            };
            return View(data);
        }

        public IActionResult DeleteStudentReport(int report)
        {
            var data = _reportDAL.GetById(report);
            if (data != null)
            {
                _reportDAL.DeleteStudentReport(data);
                return RedirectToAction("Index", "AdminStudentReport");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult UpdateStudentReport(int report)
        {
            var data = _reportDAL.GetById(report);
            if (data != null)
            {
                return View(data);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult UpdateStudentReport([FromForm] StudentReportUpdateDTO reportUpdate)
        {
            if (_reportDAL.UpdateStudentReport(reportUpdate))
            {
                return View(reportUpdate);
            }
            else
                return BadRequest();
        }
    }
}
