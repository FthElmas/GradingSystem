using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Concrete;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.Customer;
using GradingSystem.DAL.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class CustomerDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        
        public CustomerDAL()
        {
            _connectionHelper = new ConnectionHelper();
            
        }

        public List<Customer> GetAllCustomer()
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Customer";
            return conn.Query<Customer>(query).ToList();
        }

        public bool CheckCustomer(string name)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Customer where CompanyName = @CompanyName";
            var data =conn.QueryFirstOrDefault<Customer>(query, new { CompanyName = name });
            if (data == null)
                return true;
            else
                return false;
        }

        public void UpdateCustomer(Customer customer)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "update Customer set CompanyName = @CompanyName, Email = @Email, IsActive = @IsActive where CustomerID = @CustomerID";
            conn.Execute(query, new { CompanyName = customer.CompanyName, Email = customer.Email, IsActive = customer.IsActive, CustomerID = customer.CustomerID });
        }

        public Customer GetById(Guid customerID)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Customer where CustomerID = @CustomerID";
            return conn.QueryFirstOrDefault<Customer>(query, new { CustomerID = customerID });
        }

        public bool AddCustomer(Customer customer)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "insert into Customer(CustomerID, CompanyName, Email , CreatedBy, IsActive, CreatedDate) values (@customerID, @companyName, @Email,@createdBy, @isActive, @createdDate)";
                conn.QueryFirstOrDefault<Customer>(query, new { customerID = customer.CustomerID, companyName = customer.CompanyName, Email = customer.Email,createdBy = customer.CreatedBy, isActive = customer.IsActive, createdDate = customer.CreatedDate });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteCustomer(Guid _customerID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "delete from Customer where CustomerID = @customerID";
                conn.Execute(query, new { customerID = _customerID });

            }
            catch
            {
                throw new Exception();
            }
        }

        public void SoftDeleteCustomer(Customer customer)
        {
            GenericRepository<Customer> repository = new GenericRepository<Customer>();
            repository.SoftDelete(customer);
        }
    }
}
