using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.CustomerCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface ICustomerCourse
    {
        bool AddCustomerCourse(CustomerCourseAddDTO customerCourse);
        bool DeleteCustomerCourse(CustomerCourseUpdateDTO customerCourse);
        List<CustomerCourseSelectDTO> GetAll();
        bool UpdateCustomerCourse(CustomerCourseUpdateDTO customerCourse);
        CustomerCourseUpdateDTO GetById(int ID);
        List<int> GetTotalWeek(int ID);
    }
}
