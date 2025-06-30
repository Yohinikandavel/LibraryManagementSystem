using System;

namespace LibraryManagementSystem.Models
{
    public class IssueBook
    {
        public int IssueId { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
