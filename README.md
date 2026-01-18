# Online Course Enrollment – ASP.NET MVC Application

This is an ASP.NET MVC web application for managing online course enrollments (similar to Udemy).
It allows users to register candidates, edit their details, delete records, and view all registered candidates in a structured list.

The project is built using:

* ASP.NET MVC (.NET 10)
* SQL Server
* Stored Procedures for business logic
* ADO.NET for database connectivity

---

## Features

* Add new candidate
* Edit existing candidate
* Delete candidate with confirmation
* Display registered candidates with:

  * Full Name
  * Date of Birth
  * Date of Joining
  * Status (Active / Inactive)
  * Buttons for Edit & Delete
* Form validations:

  * All fields are mandatory
  * Candidate must be at least **10 years old**
* Business logic inside Stored Procedure:

  * Course prices are hardcoded
  * If Date of Joining is in the current month, **10% discount** is applied
* Success messages after Add/Edit/Delete
* Redirects to listing page after operations

---

## Technologies Used

* ASP.NET MVC (.NET 10)
* C#
* SQL Server
* Microsoft.Data.SqlClient
* Razor Views
* Stored Procedures

---

## Database Structure

### Table: `Candidates`

| Column Name | Type     |
| ----------- | -------- |
| Id          | INT (PK) |
| FullName    | VARCHAR  |
| Course      | VARCHAR  |
| Price       | DECIMAL  |
| DOB         | DATE     |
| DOJ         | DATE     |
| IsActive    | BIT      |

---

## Stored Procedure

The stored procedure `sp_SaveCandidate`:

* Inserts new records
* Updates existing records
* Calculates course price
* Applies 10% discount if DOJ is in the current month

---

## Pages

1. **Candidate Listing Page**

   * Displays all registered candidates
   * Shows Full Name, DOB, DOJ, Status
   * Provides Edit & Delete buttons

2. **Add/Edit Candidate Page**

   * Form to add or update candidate details
   * Includes validation messages

---

## Validations

* All fields are required
* Candidate must be at least 10 years old
* Date fields cannot be empty or invalid

---

## How to Run the Project

1. Clone the repository:

```bash
git clone https://github.com/riyachudasama23/OnlineCourseMVC.git
```

2. Open the project in **Visual Studio**

3. Create database in SQL Server:

```sql
CREATE DATABASE OnlineCourseDB;
```

4. Create the `Candidates` table and `sp_SaveCandidate` stored procedure in SSMS

5. Update connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "con": "Server=.\\SQLEXPRESS;Database=OnlineCourseDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

6. Run the application:

```
F5
```

The app will open directly on the candidate listing page.

---

## Learning Outcome

This project helped in understanding:

* MVC architecture
* Database connectivity using ADO.NET
* Stored Procedures for business logic
* Form validation
* CRUD operations
* Real-world ASP.NET MVC application flow

---

## Author

Riya Chudasama
Beginner ASP.NET MVC Developer

---

This project is a complete beginner-friendly demonstration of building a full-stack ASP.NET MVC application with SQL Server and Stored Procedures.
