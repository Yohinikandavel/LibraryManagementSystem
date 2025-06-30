using System;
using System.Data.SqlClient;
using LibraryManagementSystem.Database;

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
        /*
        public void ReturnBook(int issueId)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE IssueBooks SET ReturnDate = @ReturnDate WHERE IssueId = @IssueId", conn);
                cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@IssueId", issueId);

                cmd.ExecuteNonQuery();
                Console.WriteLine(" Book returned successfully.");
            }
        }
        */

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


    }
}
