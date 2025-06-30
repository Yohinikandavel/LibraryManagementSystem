using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BookService bookService = new BookService();
            IssueService issueService = new IssueService();
            AuthService authService = new AuthService();

            Console.WriteLine("===== Login =====");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User loggedInUser = authService.Login(username, password);

            if (loggedInUser == null)
            {
                Console.WriteLine(" Invalid credentials. Exiting...");
                return;
            }

            Console.WriteLine($"\n Welcome, {loggedInUser.Role}!");

            while (true)
            {
                Console.WriteLine("\n======  Library Management System ======");

                if (loggedInUser.Role == "Admin")
                {
                    Console.WriteLine("1. View All Books");
                    Console.WriteLine("2. Add New Book");
                    Console.WriteLine("3. Delete Book");
                    Console.WriteLine("4. Exit");

                    Console.Write("\nEnter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            var books = bookService.GetAllBooks();
                            Console.WriteLine("\n Available Books:");
                            foreach (var b in books)
                            {
                                Console.WriteLine($"ID: {b.BookId}, Title: {b.Title}, Author: {b.Author}, Copies: {b.CopiesAvailable}");
                            }
                            break;

                        case "2":
                            Book newBook = new Book();
                            Console.Write("Enter Title: ");
                            newBook.Title = Console.ReadLine();
                            Console.Write("Enter Author: ");
                            newBook.Author = Console.ReadLine();
                            Console.Write("Enter Copies: ");
                            newBook.CopiesAvailable = int.Parse(Console.ReadLine());
                            bookService.AddBook(newBook);
                            break;

                        case "3":
                            Console.Write("Enter Book ID to delete: ");
                            int bookId = int.Parse(Console.ReadLine());
                            bookService.DeleteBook(bookId);
                            break;

                        case "4":
                            Console.WriteLine(" Logging out...");
                            return;

                        default:
                            Console.WriteLine(" Invalid choice. Choose 1 to 4.");
                            break;
                    }
                }
                else if (loggedInUser.Role == "Student")
                {
                    Console.WriteLine("1. View All Books");
                    Console.WriteLine("2. Issue Book");
                    Console.WriteLine("3. Return Book");
                    Console.WriteLine("4. Exit");

                    Console.Write("\nEnter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            var books = bookService.GetAllBooks();
                            Console.WriteLine("\n Available Books:");
                            foreach (var b in books)
                            {
                                Console.WriteLine($"ID: {b.BookId}, Title: {b.Title}, Author: {b.Author}, Copies: {b.CopiesAvailable}");
                            }
                            break;

                        case "2":
                            Console.Write("Enter Student ID: ");
                            int sid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Book ID: ");
                            int bid = int.Parse(Console.ReadLine());
                            issueService.IssueBook(sid, bid);
                            break;

                        case "3":
                            Console.Write("Enter Issue ID: ");
                            int iid = int.Parse(Console.ReadLine());
                            issueService.ReturnBook(iid);
                            break;

                        case "4":
                            Console.WriteLine(" Logging out...");
                            return;

                        default:
                            Console.WriteLine(" Invalid choice. Choose 1 to 4.");
                            break;
                    }
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
