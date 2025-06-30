# 📚 Library Management System

A complete C# Console Application to manage books, students, and the book issuing process in a college library. Built using Visual Studio 2022, SQL Server, and NUnit for testing. Includes late return fine calculation and role-based authentication.

---

## 🚀 Features

- 🔐 **Login System** (Admin / Student)
- 📚 **View, Add, Delete Books** (Admin)
- 📖 **Issue & Return Books** (Student)
- 🧾 **Fine Calculation** (₹2/day after 14-day period)
- 🛠️ **Exception Handling** in all database operations
- 🧪 **NUnit Unit Testing** (Add, Delete, Issue, Return)
- 💾 **SQL Server Integration**
- 🖥️ **Visual Studio 2022 Console UI**

---

## 🧰 Technologies Used

- ✅ C# (.NET 8.0)
- ✅ Visual Studio 2022
- ✅ SQL Server & Management Studio
- ✅ NUnit & Test Explorer
- ✅ Git + GitHub

---

## 🗃️ Database Structure

### Tables:
- `Users`: UserId, Username, Password, Role
- `Students`: StudentId, Name, Email, ContactNumber
- `Books`: BookId, Title, Author, CopiesAvailable
- `IssueBooks`: IssueId, StudentId, BookId, IssueDate, ReturnDate
- `AuditLogs` (optional for logging actions)

---

## 🧪 Testing

All core services are tested using **NUnit**:

- ✅ Add & Delete Book  
- ✅ Issue & Return Book (valid IssueId, StudentId)  
- ✅ Passed scenarios with and without fine  

---

## 📷 Screenshots

![Test Results]
![LibMgmt jpg](https://github.com/user-attachments/assets/d5294f64-7534-4c40-837e-3c8ae00420db)

---

📌 **Sample Login Credentials**:
```txt
Admin     → Username: admin1     | Password: admin123
Student   → Username: student1   | Password: student123
