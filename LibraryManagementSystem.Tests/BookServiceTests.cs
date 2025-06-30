using NUnit.Framework;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Models;
using System.Collections.Generic;

namespace LibraryManagementSystem.Tests
{
    public class BookServiceTests
    {
        private BookService _bookService;

        [SetUp]
        public void Setup()
        {
            _bookService = new BookService();
        }

        [Test]
        public void AddBook_ShouldAddBookSuccessfully()
        {
            var book = new Book
            {
                Title = "Unit Testing in C#",
                Author = "Jane Doe",
                CopiesAvailable = 3
            };

            _bookService.AddBook(book);
            var books = _bookService.GetAllBooks();

            Assert.That(books.Exists(b => b.Title == "Unit Testing in C#"), Is.True);

        }

        [Test]
        public void DeleteBook_ShouldRemoveBook_WhenBookExists()
        {
             // Arrange
             var book = new Book
             {
                 Title = "Test Delete Book",
                 Author = "Author A",
                 CopiesAvailable = 2
             };

             _bookService.AddBook(book);
             var books = _bookService.GetAllBooks();
             var addedBook = books.Find(b => b.Title == "Test Delete Book");

             // Act
             _bookService.DeleteBook(addedBook.BookId);
             var updatedBooks = _bookService.GetAllBooks();

            // Assert
            Assert.That(updatedBooks.Exists(b => b.BookId == addedBook.BookId), Is.False);

        }
    }
}
