using LibraryManagementSystem.Database;
using LibraryManagementSystem.Models;
using System;
using System.Data.SqlClient;

namespace LibraryManagementSystem.Services
{
    public class IssueService
    {
        public void IssueBook(int studentId, int bookId)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO IssueBooks (StudentId, BookId, IssueDate) VALUES (@StudentId, @BookId, @IssueDate)", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@IssueDate", DateTime.Now);

                cmd.ExecuteNonQuery();
                Console.WriteLine(" Book issued successfully.");
            }
        }

        public void ReturnBook(int issueId)
        {
            try
            {
                using (SqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();

                    // Step 1: Get the Issue Date from DB
                    SqlCommand getIssueDateCmd = new SqlCommand("SELECT IssueDate FROM IssueBooks WHERE IssueId = @IssueId", conn);
                    getIssueDateCmd.Parameters.AddWithValue("@IssueId", issueId);

                    object result = getIssueDateCmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        Console.WriteLine(" Issue ID not found.");
                        return;
                    }

                    DateTime issueDate = (DateTime)result;
                    DateTime returnDate = DateTime.Now;

                    // Step 2: Update Return Date in DB
                    SqlCommand updateCmd = new SqlCommand("UPDATE IssueBooks SET ReturnDate = @ReturnDate WHERE IssueId = @IssueId", conn);
                    updateCmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                    updateCmd.Parameters.AddWithValue("@IssueId", issueId);
                    updateCmd.ExecuteNonQuery();

                    // Step 3: Fine Calculation
                    int gracePeriod = 14;
                    int finePerDay = 2;

                    TimeSpan difference = returnDate - issueDate;
                    int totalDays = difference.Days;

                    if (totalDays > gracePeriod)
                    {
                        int lateDays = totalDays - gracePeriod;
                        int fine = lateDays * finePerDay;
                        Console.WriteLine($" Returned late by {lateDays} days. Fine = Rs. {fine}");
                    }
                    else
                    {
                        Console.WriteLine(" Returned on time. No fine.");
                    }

                    Console.WriteLine(" Book returned successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error while returning book: " + ex.Message);
            }
        }

        public List<IssueBook> GetIssuedBooksByStudent(int studentId)
        {
            List<IssueBook> issuedBooks = new List<IssueBook>();

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM IssueBooks WHERE StudentId = @StudentId", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    issuedBooks.Add(new IssueBook
                    {
                        IssueId = (int)reader["IssueId"],
                        StudentId = (int)reader["StudentId"],
                        BookId = (int)reader["BookId"],
                        IssueDate = (DateTime)reader["IssueDate"],
                        ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : (DateTime?)reader["ReturnDate"]
                    });
                }
            }

            return issuedBooks;
        }

    }
}
