using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Course;
using GradingSystem.DTO.FilterDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Filters
{
    public class CourseActionFilter : ActionFilterAttribute
    {
        ICourseDAL _course;

        public CourseActionFilter(ICourseDAL course)
        {
            _course = course;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var reqValue = context.ActionArguments.Values.FirstOrDefault() as CourseAddDTO;

            if(reqValue != null && _course.CheckCourse(reqValue.CourseName))
            {
                await next();
            }
            else
            {
                var error = new ErrorDTO
                {
                    StatusCode = 400,
                    ErrorMessages = new List<string> { "Bu kurs zaten mevcut!!" }
                };
                context.Result = new BadRequestObjectResult(error);
            }
        }
    }
}
