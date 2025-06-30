using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

BookService bookService = new BookService();
IssueService issueService = new IssueService();
AuthService authService = new AuthService();
StudentService studentService = new StudentService();

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
        Console.WriteLine("4. Add New Student");
        Console.WriteLine("5. Exit");

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
                Student newStudent = new Student();
                Console.Write("Enter Student Name: ");
                newStudent.Name = Console.ReadLine();
                Console.Write("Enter Email: ");
                newStudent.Email = Console.ReadLine();
                Console.Write("Enter Contact Number: ");
                newStudent.ContactNumber = Console.ReadLine();
                Console.Write("Set Username for Login: ");
                string newUsername = Console.ReadLine();
                Console.Write("Set Password: ");
                string newPassword = Console.ReadLine();
                studentService.AddStudent(newStudent, newUsername, newPassword);
                break;

            case "5":
                Console.WriteLine(" Logging out...");
                return;

            default:
                Console.WriteLine(" Invalid choice. Choose 1 to 5.");
                break;
        }
    }
    else if (loggedInUser.Role == "Student")
    {
        Console.WriteLine("1. View All Books");
        Console.WriteLine("2. Issue Book");
        Console.WriteLine("3. Return Book");
        Console.WriteLine("4. View My Issued Books");
        Console.WriteLine("5. Exit");

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
                var myBooks = issueService.GetIssuedBooksByStudent(loggedInUser.StudentId.Value);
                Console.WriteLine("\n Your Issued Books:");
                foreach (var ib in myBooks)
                {
                    Console.WriteLine($"Book ID: {ib.BookId}, Issued On: {ib.IssueDate.ToShortDateString()}, Returned: {(ib.ReturnDate.HasValue ? ib.ReturnDate.Value.ToShortDateString() : "Not yet")}");
                }
                break;

            case "5":
                Console.WriteLine(" Logging out...");
                return;

            default:
                Console.WriteLine(" Invalid choice. Choose 1 to 5.");
                break;
        }
    }

    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
}
