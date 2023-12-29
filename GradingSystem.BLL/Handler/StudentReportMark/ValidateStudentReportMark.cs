using FluentValidation;
using GradingSystem.DAL.DTOs.StudentReportMark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.StudentReportMark
{
    public class ValidateStudentReportMark : AbstractValidator<StudentReportMarkDTO>
    {
        public ValidateStudentReportMark()
        {

        }
    }
}
