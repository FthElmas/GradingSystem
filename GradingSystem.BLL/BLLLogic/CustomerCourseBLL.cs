using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.CustomerCourse;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.CustomerCourse;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class CustomerCourseBLL : ICustomerCourse
    {
        CustomerCourseDAL courseDAL;
        ValidateCustomerCourse validation;
        public CustomerCourseBLL()
        {
            courseDAL = new CustomerCourseDAL();
            validation = new ValidateCustomerCourse();
        }
        public bool AddCustomerCourse(CustomerCourseAddDTO customerCourse)
        {
            MyMapper<CustomerCourseAddDTO, CustomerCourse> mapper = new MyMapper<CustomerCourseAddDTO, CustomerCourse>();
            var data = mapper.Map(customerCourse);
            ValidationResult result = validation.Validate(data);
            if (result.IsValid && courseDAL.CheckCustomerCourse(data))
            {
                return courseDAL.AddCustomerCourse(data);
            }
            else
                return false;
        }


        public bool DeleteCustomerCourse(CustomerCourseUpdateDTO customerCourse)
        {
            MyMapper<CustomerCourseUpdateDTO, CustomerCourse> mapper = new MyMapper<CustomerCourseUpdateDTO, CustomerCourse>();
            if(customerCourse.ID != 0)
            {
                return courseDAL.DeleteCustomerCourse(mapper.Map(customerCourse));
            }
            return false;
        }

        public List<CustomerCourseSelectDTO> GetAll()
        {
            MyMapper<CustomerCourse, CustomerCourseSelectDTO> mapper = new MyMapper<CustomerCourse, CustomerCourseSelectDTO>();
            List<CustomerCourseSelectDTO> courses = new List<CustomerCourseSelectDTO>();
            var data = courseDAL.GetAll().ToList();
            data.ForEach(a => courses.Add(mapper.Map(a)));
            return courses;
        }

        public CustomerCourseUpdateDTO GetById(int ID)
        {
            MyMapper<CustomerCourse, CustomerCourseUpdateDTO> mapper = new MyMapper<CustomerCourse, CustomerCourseUpdateDTO>();
            var data = courseDAL.GetById(ID);
            if(data != null)
            {
                return mapper.Map(data);
            }
            else
            {
                return null;
            }
        }

        public List<int> GetTotalWeek(int ID)
        {
            if(ID != 0)
            {
                var list = new List<int>();
                var total = courseDAL.GetTotalWeek(ID);
                for (int i = 1; i <= total; i++)
                {
                    list.Add(i);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateCustomerCourse(CustomerCourseUpdateDTO customerCourse)
        {
            MyMapper<CustomerCourseUpdateDTO, CustomerCourse> mapper = new MyMapper<CustomerCourseUpdateDTO, CustomerCourse>();
            var data = mapper.Map(customerCourse);
            ValidationResult result = validation.Validate(data);
            if(result.IsValid)
            {
                return courseDAL.UpdateCustomerCourse(data);
            }
            else
            {
                return false;
            }
        }
    }
}
