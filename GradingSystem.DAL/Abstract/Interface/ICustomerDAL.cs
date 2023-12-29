using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface ICustomerDAL
    {
        List<CustomerSelectDTO> GetAllCustomer();
        bool AddCustomer(CustomerAddDTO customer);
        void DeleteCustomer(Guid _customerID);
        bool SoftDeleteCustomer(CustomerSelectDTO customer);
        CustomerUpdateDTO GetById(Guid customerID);
        bool UpdateCustomer(CustomerUpdateDTO customer);
        bool CheckCustomer(string name);
    }
}
