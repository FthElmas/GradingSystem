using FluentValidation;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler
{
    public class ValidateCourse : AbstractValidator<Course>
    {
        public ValidateCourse()
        {
            RuleFor(dto => dto.CourseName).NotEmpty();
            RuleFor(dto => dto.IsActive).NotNull();
        }
    }
}
