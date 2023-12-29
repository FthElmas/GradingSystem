using FluentValidation;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.Teacher
{
    public class ValidateTeacher : AbstractValidator<GradingSystem.Core.Entity.Teacher>
    {
        public ValidateTeacher()
        {
            RuleFor(dto => dto.TeacherID).NotNull();
            RuleFor(dto => dto.TeacherName).NotNull();
            RuleFor(dto => dto.TeacherSurname).NotNull();
            RuleFor(dto => dto.IsActive).NotNull();
            RuleFor(dto => dto.CreatedDate).NotNull();
        }
    }
}
