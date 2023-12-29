using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Customer;
using GradingSystem.DTO.FilterDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Filters
{
    public class CustomerActionFilter : ActionFilterAttribute
    {
        ICustomerDAL _customer;
        public CustomerActionFilter(ICustomerDAL customer)
        {
            _customer = customer;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var reqValue = context.ActionArguments.Values.FirstOrDefault() as CustomerAddDTO;
            
            if(reqValue != null&&_customer.CheckCustomer(reqValue.CompanyName))
            {
                await next();
            }
            else
            {
                var error = new ErrorDTO()
                {
                    StatusCode = 400,
                    ErrorMessages = new List<string> {"Eklemeye Çalıştığınız Müşteri Zaten Bulunmaktadır!!"}
                };
                context.Result = new BadRequestObjectResult(error);
            }
        }

        
    }
}
