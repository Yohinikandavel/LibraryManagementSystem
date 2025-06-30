using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LibraryManagementSystem.Database
{
    public class DbConnection
    {
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}

