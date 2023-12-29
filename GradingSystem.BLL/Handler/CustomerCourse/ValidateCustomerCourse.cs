using FluentValidation;
using GradingSystem.DAL.DTOs.CustomerCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.CustomerCourse
{
    public class ValidateCustomerCourse : AbstractValidator<GradingSystem.Core.Entity.CustomerCourse>
    {
        public ValidateCustomerCourse()
        {
            RuleFor(dto => dto.CourseID).NotNull();
            RuleFor(dto => dto.CustomerID).NotNull();
            RuleFor(dto => dto.CreatedDate).NotNull();
            RuleFor(dto => dto.CourseStartDate).NotNull();
            RuleFor(dto => dto.CourseEndDate).NotNull();
            RuleFor(dto => dto.IsActive).NotNull();
            RuleFor(dto => dto.Name).NotNull();
            RuleFor(dto => dto.Description).NotNull().WithMessage("Description Can't be Null");
        }
    }
}
