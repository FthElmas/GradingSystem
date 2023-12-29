using GradingSystem.BLL.Filters;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Course;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DTO.DTOs.Complex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GradingSystem.Controllers
{
    public class AdminCourseController : Controller
    {
        ICourseDAL _dal;
        public AdminCourseController(ICourseDAL dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            var data = _dal.GetAllCourse();
            return View(data);
        }

        public IActionResult DeleteCourse(int course)
        {
            _dal.SoftDeleteCourse(new CourseSelectDTO { CourseID = course });
            return RedirectToAction("Index", "AdminCourse");
        }

        [HttpGet]
        public IActionResult UpdateCourse(int course, string name)
        {
            return View(new CourseUpdateDTO { CourseID = course, CourseName = name});
        }

        [HttpPost]
        public IActionResult UpdateCourse(CourseUpdateDTO courseUpdate)
        {
            if(_dal.UpdateCourse(courseUpdate))
            {
                return View(courseUpdate);
            }
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [ServiceFilter(typeof(CourseActionFilter))]
        [HttpPost]
        public IActionResult AddCourse([FromForm] CourseAddDTO course)
        {
            if (_dal.AddCourse(new CourseAddDTO {CourseName= course.CourseName, CreatedDate = DateTime.Today, IsActive = course.IsActive }, Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073")) != null)
            {
                return RedirectToAction("Index", "AdminCourse");
            }
            else
                return BadRequest();
        }

    }
}
