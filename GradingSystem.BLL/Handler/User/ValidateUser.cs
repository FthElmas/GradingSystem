using FluentValidation;
using GradingSystem.DAL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.User
{
    public class ValidateUser : AbstractValidator<GradingSystem.Core.Entity.User>
    {
        public ValidateUser()
        {
            RuleFor(dto => dto.Username).NotNull();
            RuleFor(dto => dto.UserID).NotNull();
            RuleFor(dto => dto.Email).NotNull();
            RuleFor(dto => dto.IsActive).NotNull();
            RuleFor(dto => dto.CreatedDate).NotNull();
        }
    }
}
