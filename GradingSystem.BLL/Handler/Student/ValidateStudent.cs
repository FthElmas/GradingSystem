using FluentValidation;
using GradingSystem.DAL.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.Student
{
    public class ValidateStudent : AbstractValidator<StudentAddDTO>
    {
        public ValidateStudent()
        {

            RuleFor(dto => dto.StudentID).NotEmpty().Must(x => Guid.TryParse(x.ToString(), out _)).WithMessage("This is not a guid bruv");
            RuleFor(dto => dto.StudentName).NotEmpty();
            RuleFor(dto => dto.IsEliminated).NotNull();
            RuleFor(dto => dto.IsActive).NotNull();
        }


    }
}
