using FluentValidation;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.StudentMark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.StudentMark
{
    public class ValidateStudentMark : AbstractValidator<GradingSystem.Core.Entity.StudentMark>
    {
        public ValidateStudentMark()
        {
            RuleFor(dto => dto.CreatedDate).NotNull();
            RuleFor(dto => dto.StudentID).NotNull();
            RuleFor(dto => dto.Mark).NotNull();
        }
    }
}
