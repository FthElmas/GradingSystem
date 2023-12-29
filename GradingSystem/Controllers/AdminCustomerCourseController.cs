using GradingSystem.BLL.Services;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Customer;
using GradingSystem.DAL.DTOs.CustomerCourse;
using GradingSystem.DTO.DTOs.Complex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GradingSystem.Controllers
{
    public class AdminCustomerCourseController : Controller
    {
        ICustomerCourse _dal;
        ICourseDAL _coursedal;
        ICustomerDAL _customerdal;
        public AdminCustomerCourseController(ICustomerCourse dal, ICourseDAL coursedal, ICustomerDAL customerdal)
        {
            _dal = dal;
            _coursedal = coursedal;
            _customerdal = customerdal;
        }
        public IActionResult Index()
        {
            var data = _dal.GetAll();
            return View(data);
        }

        public IActionResult DeleteCustomerCourse(int customerCourse)
        {
            _dal.DeleteCustomerCourse(new CustomerCourseUpdateDTO { ID = customerCourse });
            return RedirectToAction("Index", "AdminCustomer");
        }

        [HttpGet]
        public IActionResult UpdateCustomerCourse(int customerCourse)
        {
            var data = _dal.GetById(customerCourse);
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateCustomerCourse([FromForm] CustomerCourseUpdateDTO customerUpdate)
        {
            if (_dal.UpdateCustomerCourse(customerUpdate))
            {
                return View(customerUpdate);
            }
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult AddCustomerCourse()
        {
            var courseList = _coursedal.GetAllCourse();
            var customerList = _customerdal.GetAllCustomer();

            CustomerCourseAddPage customerCourse = new CustomerCourseAddPage()
            {
                CourseList = courseList.Select(a => new SelectListItem
                {
                    Value = a.CourseID.ToString(),
                    Text = a.CourseName
                }).ToList(),
                CustomerList = customerList. Select(a => new SelectListItem
                {
                    Value = a.CustomerID.ToString(),
                    Text = a.CompanyName
                }).ToList()
            };
            return View(customerCourse);
        }

        [HttpPost]
        public IActionResult AddCustomerCourse([FromForm] CustomerCourseAddPage customer)
        {
            if(_dal.AddCustomerCourse(new CustomerCourseAddDTO {Name = customer.CustomerCourse.Name, Description = customer.CustomerCourse.Description, CourseID = customer.CustomerCourse.CourseID, CustomerID = customer.CustomerCourse.CustomerID, CourseStartDate = customer.CustomerCourse.CourseStartDate, CourseEndDate = customer.CustomerCourse.CourseEndDate, CreatedDate = DateTime.Today, IsActive = true }))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
