using FluentValidation;
using GradingSystem.DAL.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.Project
{
    public class ValidateProjectUpdate : AbstractValidator<ProjectUpdateDTO>
    {
        public ValidateProjectUpdate()
        {
            RuleFor(dto => dto.ProjectID).NotNull();
            RuleFor(dto => dto.ProjectName).NotEmpty();
            RuleFor(dto => dto.IsActive).NotEmpty();
            RuleFor(dto => dto.ProjectStartDate).NotNull();
            RuleFor(dto => dto.ProjectEndDate).NotNull();
        }
    }
}
