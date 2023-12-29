using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DAL.DTOs.StudentQuiz;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.Controllers
{
    public class AdminStudentProjectController : Controller
    {
        IStudentProjectDAL _dal;
        public AdminStudentProjectController(IStudentProjectDAL dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            var data = _dal.GetAll();
            return View(data);
        }

        public IActionResult DeleteStudentProject(Guid student, int project)
        {
            var data = _dal.GetByID(student, project);
            if (data != null)
            {
                _dal.DeleteStudentProjectWithMark(data);
                return RedirectToAction("Index", "AdminStudentProject");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult UpdateStudentProject(Guid student, int project)
        {
            var data = _dal.GetByID(student, project);
            if (data != null)
            {
                return View(data);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult UpdateStudentQuiz([FromForm] StudentProjectDTO projectUpdate, int mark)
        {
            if (_dal.UpdateStudentProjectWithMark(projectUpdate, mark))
            {
                return View(projectUpdate);
            }
            else
                return BadRequest();
        }
    }
}
