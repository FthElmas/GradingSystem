using FluentValidation;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.StudentQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.StudentQuiz
{
    public class ValidateStudentQuiz : AbstractValidator<GradingSystem.Core.Entity.StudentQuiz>
    {
        public ValidateStudentQuiz()
        {
            RuleFor(dto => dto.StudentID).NotNull();
            RuleFor(dto => dto.QuizID).NotNull();
            RuleFor(dto => dto.Description).NotEmpty();
        }
    }
}
