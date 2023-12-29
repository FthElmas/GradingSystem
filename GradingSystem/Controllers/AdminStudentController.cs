using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Extensions;
using GradingSystem.BLL.Filters;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DTO.DTOs.Complex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GradingSystem.Controllers
{
    public class AdminStudentController : Controller
    {
        IStudentDAL _student;
        private readonly ICustomerCourse _customerCourse;
        public AdminStudentController(IStudentDAL student, ICustomerCourse customerCourse)
        {
            _student = student;
            _customerCourse = customerCourse;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = _student.GetAllStudentOfTeacher(Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"));
            return View(data);
        }


        public IActionResult DeleteStudent(Guid student)
        {       
            _student.SoftDeleteStudent(new StudentSelectDTO { StudentID = student });
            return RedirectToAction("Index", "AdminStudent");
        }

        [HttpGet]
        public IActionResult UpdateStudent(Guid student, string name, string surname)
        {
            var data = new StudentUpdateDTO { StudentID = student, StudentName = name, StudentSurname = surname };
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateStudent(StudentUpdateDTO studentUpdate)
        {
            if (_student.UpdateStudent(studentUpdate))
            {
                return View();
            }
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            var list = _customerCourse.GetAll().ToList();
            StudentAdd student = new StudentAdd()
            {
                CustomerCourseList = list.Select(x=> new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.Name
                }).ToList()
            };
            return View(student);
        }

        [ServiceFilter(typeof(StudentActionFilter))]
        [HttpPost]
        public IActionResult AddStudent([FromForm]StudentAdd studentos)
        {
            if (_student.AddStudentWithStudentTeacher(new StudentAddDTO { StudentID = Guid.NewGuid(), StudentName = studentos.Student.StudentName, StudentSurname = studentos.Student.StudentSurname, CustomerCourseID = studentos.Student.CustomerCourseID, Email = studentos.Student.Email, GraduationState = studentos.Student.GraduationState, IsEliminated = studentos.Student.IsEliminated, IsActive = studentos.Student.IsActive, CreatedDate = DateTime.Today}, Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073")))
            {
                return RedirectToAction("Index", "AdminStudent");
            }
            else
                return BadRequest();
        }

        //[HttpPost]
        //public IActionResult UpdateStudent(StudentUpdateDTO student)
        //{
        //    _student.UpdateStudent(student);
        //    return RedirectToAction("Index", "Student");
        //}

    }
}
