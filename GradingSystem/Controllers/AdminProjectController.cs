using GradingSystem.BLL.Extensions;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Customer;
using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.DTOs.Student;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.Controllers
{
    public class AdminProjectController : Controller
    {
        IProjectDAL _dal;
        public AdminProjectController(IProjectDAL dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            var data = _dal.GetAll();
            return View(data);
        }

        public IActionResult DeleteProject(int project)
        {
            if(project != 0)
            {
                _dal.DeleteProject(new ProjectUpdateDTO { ProjectID = project });
                return RedirectToAction("Index", "AdminProject");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult UpdateProject(int project)
        {
            var data = _dal.GetById(project);
            if(data != null)
            {
                return View(data);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult UpdateProject([FromForm] ProjectUpdateDTO projectUpdate)
        {
            if (_dal.UpdateProject(projectUpdate))
            {
                return View(projectUpdate);
            }
            else
                return BadRequest();
        }
    }
}
