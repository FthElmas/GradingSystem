using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GradingSystem.DAL.Concrete
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using GradingSystem.DAL.Abstract.Interface;
    using static Dapper.SqlMapper;

    namespace DapperGenericRepository.Repository
    {
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            public IDbConnection _connection;

            readonly string connectionString = "server = .; Database = GradingSystemDB; Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True";

            public GenericRepository()
            {
                _connection = new SqlConnection(connectionString);
            }

            //public bool Add(T entity)
            //{
            //    int rowsEffected = 0;
            //    try
            //    {
            //        string tableName = GetTableName();
            //        string columns = GetColumns(excludeKey: true);
            //        string properties = GetPropertyNames(excludeKey: true);
            //        string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

            //        rowsEffected = _connection.Execute(query, entity);
            //    }
            //    catch (Exception ex) { }

            //    return rowsEffected > 0 ? true : false;
            //}

            public bool Add(T entity)
            {
                int rowsEffected = 0;
                try
                {
                    string tableName = GetTableName();
                    string columns = GetColumns(excludeKey: true);
                    string properties = GetPropertyNames(excludeKey: true);
                    string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

                    rowsEffected = _connection.Execute(query, entity);
                }
                catch (Exception ex) { }

                return rowsEffected > 0 ? true : false;
            }

            public T ReturnAdd(T entity)
            {
                
                try
                {
                    string tableName = GetTableName();
                    string columns = GetColumns(excludeKey: true);
                    string properties = GetPropertyNames(excludeKey: true);
                    string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";
                    var data = _connection.QueryFirstOrDefault<T>(query, entity);
                    return data;
                }
                catch (Exception ex) { return null; }

                
            }

            public bool Delete(T entity)
            {
                int rowsEffected = 0;
                try
                {
                    string tableName = GetTableName();
                    string keyColumn = GetKeyColumnName();
                    string keyProperty = GetKeyPropertyName();
                    string query = $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyProperty}";

                    rowsEffected = _connection.Execute(query, entity);
                }
                catch (Exception ex) { }

                return rowsEffected > 0 ? true : false;
            }

            public bool SoftDelete(T entity)
            {
                int rowsEffected = 0;
                try
                {
                    string tableName = GetTableName();
                    string keyColumn = GetKeyColumnName();
                    string keyProperty = GetKeyPropertyName();

                    StringBuilder query = new StringBuilder();
                    query.Append($"UPDATE {tableName} SET ");

                   
                    query.Append("IsActive = 0");
                    

                    

                    query.Append($" WHERE {keyColumn} = @{keyProperty}");

                    rowsEffected = _connection.Execute(query.ToString(), entity);
                }
                catch (Exception ex) { }

                return rowsEffected > 0 ? true : false;
            }

            public IEnumerable<T> GetAll()
            {
                IEnumerable<T> result = null;
                try
                {
                    string tableName = GetTableName();
                    string query = $"SELECT * FROM {tableName} WHERE IsActive = 1";

                    result = _connection.Query<T>(query);
                }
                catch (Exception ex) { }

                return result;
            }

            public IEnumerable<T> GetAll(string columnName, Guid ID)
            {
                IEnumerable<T> result = null;
                try
                {
                    string tableName = GetTableName();
                    string query = $"SELECT * FROM {tableName} WHERE {columnName} = ID and IsActive = 1";

                    result = _connection.Query<T>(query);
                }
                catch (Exception ex) { }

                return result;
            }

            public IEnumerable<T> GetAll(string columnName, int ID)
            {
                IEnumerable<T> result = null;
                try
                {
                    string tableName = GetTableName();
                    string query = $"SELECT * FROM {tableName} WHERE {columnName} = ID and IsActive = 1";

                    result = _connection.Query<T>(query);
                }
                catch (Exception ex) { }

                return result;
            }

            public T GetById(int Id)
            {
                IEnumerable<T> result = null;
                try
                {
                    string tableName = GetTableName();
                    string keyColumn = GetKeyColumnName();
                    string query = $"SELECT * FROM {tableName} WHERE {keyColumn} = '{Id}' and IsActive = 1";

                    result = _connection.Query<T>(query);
                }
                catch (Exception ex) { }

                return result.FirstOrDefault();
            }

            public bool Update(T entity)
            {
                int rowsEffected = 0;
                try
                {
                    string tableName = GetTableName();
                    string keyColumn = GetKeyColumnName();
                    string keyProperty = GetKeyPropertyName();

                    StringBuilder query = new StringBuilder();
                    query.Append($"UPDATE {tableName} SET ");

                    foreach (var property in GetProperties(true))
                    {
                        var columnAttr = property.GetCustomAttribute<ColumnAttribute>();
                        if (!property.IsDefined(typeof(ForeignKeyAttribute)) && !property.IsDefined(typeof(NotMappedAttribute)))
                        {
                            string propertyName = property.Name;
                            string columnName = columnAttr?.Name ?? propertyName;

                            query.Append($"{columnName} = @{propertyName},");
                        }
                        else
                            continue;
                    }

                    query.Remove(query.Length - 1, 1);

                    query.Append($" WHERE {keyColumn} = @{keyProperty}");

                    rowsEffected = _connection.Execute(query.ToString(), entity);
                }
                catch (Exception ex) { }

                return rowsEffected > 0 ? true : false;
            }

            private string GetTableName()
            {
                string tableName = "";
                var type = typeof(T);
                var tableAttr = type.GetCustomAttribute<TableAttribute>();
                if (tableAttr != null)
                {
                    tableName = tableAttr.Name;
                    return tableName;
                }

                return type.Name + "s";
            }

            public static string GetKeyColumnName()
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

                    if (keyAttributes != null && keyAttributes.Length > 0)
                    {
                        object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                        if (columnAttributes != null && columnAttributes.Length > 0)
                        {
                            ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                            return columnAttribute.Name;
                        }
                        else
                        {
                            return property.Name;
                        }
                    }
                }

                return null;
            }


            private string GetColumns(bool excludeKey = false)
            {
                var type = typeof(T);
                var columns = string.Join(", ", type.GetProperties()
                    .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)) && !p.IsDefined(typeof(NotMappedAttribute)))
                    .Select(p =>
                    {
                        var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                        return columnAttr != null ? columnAttr.Name : p.Name;
                    }));

                return columns;
            }

            protected string GetPropertyNames(bool excludeKey = false)
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null && !p.IsDefined(typeof(NotMappedAttribute)));

                var values = string.Join(", ", properties.Select(p =>
                {
                    return $"@{p.Name}";
                }));

                return values;
            }

            protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null && !p.IsDefined(typeof(NotMappedAttribute)));

                return properties;
            }

            protected string GetKeyPropertyName()
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => p.GetCustomAttribute<KeyAttribute>() != null && !p.IsDefined(typeof(NotMappedAttribute)));

                if (properties.Any())
                {
                    return properties.FirstOrDefault().Name;
                }

                return null;
            }
        }
    }
}
