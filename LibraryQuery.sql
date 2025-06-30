CREATE DATABASE LibraryDB;

USE LibraryDB;

-- USERS TABLE
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) CHECK (Role IN ('Admin', 'Student')) NOT NULL
);

-- STUDENTS TABLE
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    ContactNumber NVARCHAR(15)
);

-- BOOKS TABLE
CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100),
    Author NVARCHAR(100),
    CopiesAvailable INT CHECK (CopiesAvailable >= 0)
);

-- ISSUE TABLE
CREATE TABLE IssueBooks (
    IssueId INT PRIMARY KEY IDENTITY,
    StudentId INT FOREIGN KEY REFERENCES Students(StudentId),
    BookId INT FOREIGN KEY REFERENCES Books(BookId),
    IssueDate DATE NOT NULL,
    ReturnDate DATE NULL
);

-- AUDIT LOGS TABLE
CREATE TABLE AuditLogs (
    LogId INT PRIMARY KEY IDENTITY,
    Action NVARCHAR(100),
    UserId INT,
    Timestamp DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(255)
);

-- Sample Admin and Student Login
INSERT INTO Users (Username, Password, Role) VALUES 
('admin1', 'admin123', 'Admin'),
('student1', 'student123', 'Student');

-- Sample Books
INSERT INTO Books (Title, Author, CopiesAvailable) VALUES 
('C# Programming', 'John Sharp', 5),
('SQL Essentials', 'Ben Forta', 3),
('Clean Code', 'Robert C. Martin', 2);

-- Sample Students
INSERT INTO Students (Name, Email, ContactNumber) VALUES
('Ananya Rao', 'ananya@gmail.com', '9876543210'),
('Ravi Kumar', 'ravi.kumar@gmail.com', '9123456789');

-- Ananya issued SQL Essentials on June 15, hasn't returned yet
INSERT INTO IssueBooks (StudentId, BookId, IssueDate, ReturnDate) 
VALUES (1, 2, '2025-06-15', NULL);

-- Ravi issued Clean Code on June 1, returned on June 10
INSERT INTO IssueBooks (StudentId, BookId, IssueDate, ReturnDate) 
VALUES (2, 3, '2025-06-01', '2025-06-10');

SELECT * FROM Users;
SELECT * FROM Books;
SELECT * FROM Students;
SELECT * FROM IssueBooks;

