using FluentValidation;
using GradingSystem.DAL.DTOs.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.Quiz
{
    public class ValidateQuiz : AbstractValidator<QuizAddDTO>
    {
        public ValidateQuiz()
        {
            RuleFor(dto => dto.QuizName).NotNull();
            RuleFor(dto => dto.QuizDescription).NotNull();
            RuleFor(dto => dto.CreatedDate).NotNull();
            RuleFor(dto => dto.IsActive).NotNull();
        }
    }
}
