using Dapper;
using GradingSystem.DAL.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Concrete
{
    public class Repository<T>
    {
        private readonly ConnectionHelper _connectionHelper;
        public Repository(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public IEnumerable<T> GetAll(string query)
        {
            using var conn = _connectionHelper.CreateConnection();
            var list = conn.Query<T>(query);
            return list.ToList();
        }

        public T GetByID(string query, object? parameters )
        {
            using var conn = _connectionHelper.CreateConnection();
            return conn.QueryFirstOrDefault<T>(query, parameters);
        }
        public T Add(string query, object? parameters)
        {
            using var conn = _connectionHelper.CreateConnection();
            return conn.QueryFirstOrDefault<T>(query, parameters);
        }

        public void HardDelete(string query, object? parameters)
        {
            using var conn = _connectionHelper.CreateConnection();
            conn.Execute(query, parameters);
        }

        public void Update(string query, object? parameters)
        {
            using var conn = _connectionHelper.CreateConnection();
            conn.Execute(query, parameters);
        }

        public void SoftDelete(string query, object? parameters)
        {
            using var conn = _connectionHelper.CreateConnection();
            conn.Execute(query, parameters);
        }

    }
}
