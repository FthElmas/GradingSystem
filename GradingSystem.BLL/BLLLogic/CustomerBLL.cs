using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.Customer;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Customer;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class CustomerBLL : ICustomerDAL
    {
        CustomerDAL customerDAL;
        ValidateCustomer validation;
        public CustomerBLL()
        {
            customerDAL = new CustomerDAL();
            validation = new ValidateCustomer();
        }

        public bool AddCustomer(CustomerAddDTO customer)
        {
            MyMapper<CustomerAddDTO, Customer> mapper = new MyMapper<CustomerAddDTO, Customer>();
            var data = mapper.Map(customer);
            ValidationResult result = validation.Validate(data);
            if(result.IsValid)
            {
                return customerDAL.AddCustomer(data);
            }
            return false;
        }

        public bool CheckCustomer(string name)
        {
            if (name != null)
                return customerDAL.CheckCustomer(name);

            return false;
        }

        public void DeleteCustomer(Guid _customerID)
        {
           if(_customerID != Guid.Empty)
           {
                customerDAL.DeleteCustomer((Guid)_customerID);
           }
           
        }



        public List<CustomerSelectDTO> GetAllCustomer()
        {
            MyMapper<Customer, CustomerSelectDTO> mapper = new MyMapper<Customer, CustomerSelectDTO>();
            List<CustomerSelectDTO> list = new List<CustomerSelectDTO>();
            var data = customerDAL.GetAllCustomer();
            data.ForEach(a => list.Add(mapper.Map(a)));
            return list;
        }

        public CustomerUpdateDTO GetById(Guid customerID)
        {
            MyMapper<Customer, CustomerUpdateDTO> mapper = new MyMapper<Customer, CustomerUpdateDTO>();
            if (customerID != Guid.Empty)
            {
                var data = customerDAL.GetById(customerID);
                return mapper.Map(data);
            }
            else
                return null;
        }

        public bool SoftDeleteCustomer(CustomerSelectDTO customer)
        {

            MyMapper<CustomerSelectDTO, Customer> mapper = new MyMapper<CustomerSelectDTO, Customer>();
            if (customer.CustomerID != Guid.Empty)
            {
                customerDAL.SoftDeleteCustomer(mapper.Map(customer));
                return true;
            }
            return false;

        }

        public bool UpdateCustomer(CustomerUpdateDTO customer)
        {
            MyMapper<CustomerUpdateDTO, Customer> mapper = new MyMapper<CustomerUpdateDTO, Customer>();
            var data = mapper.Map(customer);
            ValidationResult result = validation.Validate(data);
            if(result.IsValid)
            {
                customerDAL.UpdateCustomer(data);
                return true;
            }
            return false;


        }
    }
}
