using FluentValidation;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.StudentProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.StudentProject
{
    public class ValidateStudentProject : AbstractValidator<GradingSystem.Core.Entity.StudentProject>
    {
        public ValidateStudentProject()
        {
            RuleFor(dto => dto.ProjectID).NotNull();
            RuleFor(dto => dto.StudentID).NotNull();
            RuleFor(dto => dto.Description).NotEmpty();
            RuleFor(dto => dto.CreatedDate).NotNull();
        }
    }
}
