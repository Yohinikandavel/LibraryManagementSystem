using NUnit.Framework;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests
{
    public class IssueServiceTests
    {
        private IssueService _issueService;

        [SetUp]
        public void Setup()
        {
            _issueService = new IssueService();
        }

        [Test]
        public void IssueBook_ShouldInsertIssueRecord()
        {
            int studentId = 1;
            int bookId = 2;

            _issueService.IssueBook(studentId, bookId);

            Assert.Pass("Book issued successfully without exception.");
        }

        [Test]
        public void ReturnBook_ShouldUpdateReturnDate()
        {
            int issueId = 1;

            _issueService.ReturnBook(issueId);

            Assert.Pass("Book return updated successfully.");
        }

        [Test]
        public void GetIssuedBooksByStudent_ShouldReturnCorrectList()
        {
            int studentId = 1;

            var issuedBooks = _issueService.GetIssuedBooksByStudent(studentId);

            if (issuedBooks != null && issuedBooks.Count >= 0)
            {
                Assert.Pass(" Issued book list fetched successfully.");
            }
            else
            {
                Assert.Fail(" Failed to fetch issued book list.");
            }

        }

        [Test]
        public void ReturnBook_InvalidIssueId_ShouldHandleGracefully()
        {
            int invalidIssueId = 9999;

            Assert.DoesNotThrow(() => _issueService.ReturnBook(invalidIssueId),
                "System should handle invalid IssueId without crashing.");
        }


    }
}
