using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.CustomerCourse;
using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class CustomerCourseDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public CustomerCourseDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }
        public bool AddCustomerCourse(CustomerCourse customerCourse)
        {
            try
            {
                GenericRepository<CustomerCourse> repo = new GenericRepository<CustomerCourse>();
                repo.Add(customerCourse);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public bool CheckCustomerCourse(CustomerCourse customerCourse)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from CustomerCourse where Name = @Name";
            if (conn.QueryFirstOrDefault(query, new { Name = customerCourse.Name  }) == null)
            {
                return true;
            }
            return false;
        }

        public CustomerCourse GetById(int ID)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from [CustomerCourse] where ID = @ID";
            return conn.QueryFirstOrDefault<CustomerCourse>(query, new { ID = ID });
        }

        public int? GetTotalWeek(int ID)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select DATEDIFF(WEEK, CourseStartDate, CourseEndDate) as TotalWeek from CustomerCourse where ID = @ID";
            return conn.ExecuteScalar<int?>(query, new { ID = ID });
        }

        public bool DeleteCustomerCourse(CustomerCourse customerCourse)
        {
            try
            {
                GenericRepository<CustomerCourse> repo = new GenericRepository<CustomerCourse>();
                repo.SoftDelete(customerCourse);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public List<CustomerCourse> GetAll()
        {
            GenericRepository<CustomerCourse> repo = new GenericRepository<CustomerCourse>();
            return repo.GetAll().ToList();
        }

        public bool UpdateCustomerCourse(CustomerCourse customerCourse)
        {
            try
            {
                GenericRepository<CustomerCourse> repo = new GenericRepository<CustomerCourse>();
                repo.Update(customerCourse);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
