using FluentValidation;
using GradingSystem.DAL.DTOs.StudentReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.StudentReport
{
    public class ValidateStudentReport : AbstractValidator<GradingSystem.Core.Entity.StudentReport>
    {
        public ValidateStudentReport()
        {
            RuleFor(dto => dto.ReportName).NotNull();
            RuleFor(dto => dto.Report).NotNull();
            RuleFor(dto => dto.CreatedDate).NotNull();
            RuleFor(dto => dto.StudentID).NotNull();
            RuleFor(dto => dto.CreatedWeek).NotNull();
        }
    }
}
