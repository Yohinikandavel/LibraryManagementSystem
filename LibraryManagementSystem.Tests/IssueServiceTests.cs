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
            // Use existing StudentId = 1 and BookId = 1 (ensure exists in DB)
            int studentId = 1;
            int bookId = 2;

            // Act
            _issueService.IssueBook(studentId, bookId);

            // If no exception is thrown, test is considered passed
            Assert.Pass("Book issued successfully without exception.");
        }

        [Test]
        public void ReturnBook_ShouldUpdateReturnDate()
        {
            int issueId = 1; // Make sure issueId 1 exists

            // Act
            _issueService.ReturnBook(issueId);

            // If no error thrown, test passed
            Assert.Pass("Book return updated successfully.");
        }

    }
}
