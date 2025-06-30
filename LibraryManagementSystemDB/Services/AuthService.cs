using System.Data.SqlClient;
using LibraryManagementSystem.Database;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class AuthService
    {
        public User Login(string username, string password)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @username AND Password = @password", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        StudentId = reader["StudentId"] == DBNull.Value ? null : (int?)reader["StudentId"]
                    };
                }
            }
            return null;
        }

    }
}
