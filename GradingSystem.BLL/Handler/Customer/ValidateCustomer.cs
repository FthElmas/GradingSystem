using FluentValidation;
using GradingSystem.DAL.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Handler.Customer
{
    public class ValidateCustomer : AbstractValidator<GradingSystem.Core.Entity.Customer>
    {
        public ValidateCustomer()
        {
            RuleFor(dto => dto.CompanyName).NotEmpty();
            RuleFor(dto => dto.Email).NotEmpty();
            RuleFor(dto => dto.IsActive).NotNull();

        }
    }
}
