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
        public void DeleteBook_ShouldRemoveBook_WhenBookExists()
        {
             var book = new Book
             {
                 Title = "Test Delete Book",
                 Author = "Author A",
                 CopiesAvailable = 2
             };

             _bookService.AddBook(book);
             var books = _bookService.GetAllBooks();
             var addedBook = books.Find(b => b.Title == "Test Delete Book");

             _bookService.DeleteBook(addedBook.BookId);
             var updatedBooks = _bookService.GetAllBooks();

            Assert.That(updatedBooks.Exists(b => b.BookId == addedBook.BookId), Is.False);

        }
    }
}
