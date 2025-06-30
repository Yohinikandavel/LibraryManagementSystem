using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LibraryManagementSystem.Database;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class BookService
    {
        // View all books
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        BookId = (int)reader["BookId"],
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        CopiesAvailable = (int)reader["CopiesAvailable"]
                    });
                }
            }

            return books;
        }

        // Add a new book
        public void AddBook(Book book)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Books (Title, Author, CopiesAvailable) VALUES (@Title, @Author, @Copies)", conn);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Copies", book.CopiesAvailable);

                cmd.ExecuteNonQuery();
                Console.WriteLine(" Book added successfully.");
            }
        }

        // Delete a book
        public void DeleteBook(int bookId)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE BookId = @BookId", conn);
                cmd.Parameters.AddWithValue("@BookId", bookId);

                cmd.ExecuteNonQuery();
                Console.WriteLine(" Book deleted successfully.");
            }
        }
    }
}

