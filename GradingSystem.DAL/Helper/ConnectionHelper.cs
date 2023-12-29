using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GradingSystem.DAL.Helper
{
    public class ConnectionHelper
    {
        private IDbConnection _conf;
        readonly string connectionString = "server = .; Database = GradingSystemDB; Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True";
        public ConnectionHelper()
        {
            
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public void CloseConnection(IDbConnection connection)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
