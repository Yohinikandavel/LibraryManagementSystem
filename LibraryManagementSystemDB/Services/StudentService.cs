using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LibraryManagementSystem.Database;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class StudentService
    {
        public void AddStudent(Student student, string username, string password)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Students (Name, Email, ContactNumber) OUTPUT INSERTED.StudentId VALUES (@Name, @Email, @Contact)", conn);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Contact", student.ContactNumber);

                int studentId = (int)cmd.ExecuteScalar();

                // Create login
                SqlCommand loginCmd = new SqlCommand("INSERT INTO Users (Username, Password, Role, StudentId) VALUES (@Username, @Password, 'Student', @StudentId)", conn);
                loginCmd.Parameters.AddWithValue("@Username", username);
                loginCmd.Parameters.AddWithValue("@Password", password);
                loginCmd.Parameters.AddWithValue("@StudentId", studentId);
                loginCmd.ExecuteNonQuery();

                Console.WriteLine(" Student and login created successfully.");
            }
        }
    }
}
