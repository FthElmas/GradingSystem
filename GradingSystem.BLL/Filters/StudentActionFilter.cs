using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DTO.DTOs.Complex;
using GradingSystem.DTO.FilterDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Filters
{
    public class StudentActionFilter : ActionFilterAttribute
    {
        IStudentDAL _student;
        public StudentActionFilter(IStudentDAL student)
        {
            _student = student;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var reqValue = context.ActionArguments.Values.FirstOrDefault() as StudentAdd;

            if(reqValue != null && _student.CheckStudent(reqValue.Student))
            {
                await next();
            }
            else
            {
                var error = new ErrorDTO
                {
                    StatusCode = 400,
                    ErrorMessages = new List<string> { "Eklemeye çalıştığınız öğrenci zaten mevcut!!" }
                };

                context.Result = new BadRequestObjectResult(error);
            }
        }
    }
}
