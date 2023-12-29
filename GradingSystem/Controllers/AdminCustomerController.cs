using GradingSystem.BLL.Filters;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Course;
using GradingSystem.DAL.DTOs.Customer;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.Controllers
{
    public class AdminCustomerController : Controller
    {
        ICustomerDAL _dal;
        public AdminCustomerController(ICustomerDAL dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            var data = _dal.GetAllCustomer();
            return View(data);
        }

        public IActionResult DeleteCustomer(Guid customer)
        {
            _dal.SoftDeleteCustomer(new DAL.DTOs.Customer.CustomerSelectDTO { CustomerID = customer });
            return RedirectToAction("Index", "AdminCustomer");
        }

        [HttpGet]
        public IActionResult UpdateCustomer(Guid customer)
        {
            var data = _dal.GetById(customer);
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateCustomer([FromForm]CustomerUpdateDTO customerUpdate)
        {
            if (_dal.UpdateCustomer(customerUpdate))
            {
                return View(customerUpdate);
            }
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [ServiceFilter(typeof(CustomerActionFilter))]
        [HttpPost]
        public IActionResult AddCustomer([FromForm] CustomerAddDTO customer)
        {
            if (_dal.AddCustomer(new CustomerAddDTO {CustomerID = Guid.NewGuid() ,CompanyName = customer.CompanyName, Email = customer.Email ,CreatedDate = DateTime.Today, IsActive = customer.IsActive }))
            {
                return RedirectToAction("Index", "AdminCustomer");
            }
            else
                return BadRequest();
        }

    }
}
