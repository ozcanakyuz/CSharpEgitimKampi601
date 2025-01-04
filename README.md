# Employee Management Application

This project is a Windows Forms Application built using **.NET** with a **PostgreSQL** database connection. The application features a form (`FrmEmployee`) that enables managing employees and departments through CRUD operations and ID-based data retrieval.

## Features
- 🎯 **List Employees and Departments:** Displays employee details along with department names.
- ➕ **Add Employees:** Allows adding new employee records to the database.
- ✏️ **Update Employee Records:** Modify existing employee information.
- ❌ **Delete Employees:** Remove employees from the database.
- 🔍 **Find by ID:** Retrieve specific employee details using their ID.

## Database Details
The application uses two tables:
1. **Employees:** Stores employee information with the following columns:
   - `EmployeeId`
   - `EmployeeName`
   - `EmployeeSurname`
   - `EmployeeSalary`
   - `DepartmentId`
   - `DepartmentName` (via JOIN with `Departments` table)
2. **Departments:** Stores department details with the columns:
   - `DepartmentId`
   - `DepartmentName`

## Data Display
The data displayed in the application includes the following columns:
- EmployeeId
- EmployeeName
- EmployeeSurname
- EmployeeSalary
- DepartmentId
- DepartmentName

## Screenshots
For example:
![Employee Management Form](https://i.hizliresim.com/fb316i7.png)

---

## Usage
This project demonstrates:
- 🔗 Connecting a .NET application to PostgreSQL using Npgsql.
- 🛠️ Executing SQL queries for CRUD operations.
- 🖥️ Implementing a Windows Forms interface for database interaction.

Feel free to customize or enhance the application for your needs!

