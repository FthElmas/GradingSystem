using FluentValidation;
using GradingSystem.DAL.DTOs.StudentTeacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.StudentTeacher
{
    public class ValidateStudentTeacher : AbstractValidator<StudentTeacherDTO>
    {
        public ValidateStudentTeacher()
        {
            RuleFor(dto => dto.TeacherID).NotNull();
            RuleFor(dto => dto.StudentID).NotNull();
            RuleFor(dto => dto.IsActive).NotNull();
        }
    }
}
