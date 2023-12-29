using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DTO.DTOs.Complex;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ICustomerCourse _customerCourse;
        public NavbarViewComponent(ICustomerCourse customerCourse)
        {
            _customerCourse = customerCourse;
        }

        public IViewComponentResult Invoke()
        {
            var customer = _customerCourse.GetAll().ToList();
            return View(customer);
        }
    }
}
