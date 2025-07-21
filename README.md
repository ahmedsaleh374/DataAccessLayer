Employee Management System – ASP.NET Core MVC (N-Tier Architecture)
This is a simple Employee and Department Management System built with ASP.NET Core MVC.
The project is part of my personal learning journey in enterprise-level software architecture using .NET technologies.
🎯 Learning Goals
•	✅ N-Tier Architecture (Presentation, Business Logic, Data Access, Domain)
•	✅ Generic Repository Pattern
•	✅ Unit of Work Pattern
•	✅ AutoMapper for mapping DTOs and entities
•	✅ Entity Framework Core with LINQ and Migrations
•	✅ Dependency Injection
•	✅ Clean Code and separation of concerns
🛠️ Technologies Used
•	- ASP.NET Core MVC (.NET 8)
•	- Entity Framework Core
•	- AutoMapper
•	- SQL Server / LocalDB
•	- Bootstrap 5
•	- Font Awesome
•	- LINQ
•	- Visual Studio 2022+

📁 Project Structure
/EmployeeSystem
│
├── EmployeeSystem.Web        → Presentation Layer (MVC)
├── EmployeeSystem.BLL        → Business Logic Layer (Services)
├── EmployeeSystem.DAL        → Data Access Layer (Repositories)
├── EmployeeSystem.Domain     → Entity Models + Interfaces
├── EmployeeSystem.Data       → EF Core Context + Migrations

⚙️ Getting Started
•	• Clone the repository: git clone (https://github.com/ahmedsaleh374/DataAccessLayer)
•	• Open the solution in Visual Studio 2022 (or newer)
•	• Update your SQL connection string in appsettings.json
•	• Apply EF Core Migrations: Update-Database
•	• Run the project and start exploring!

🚧 Project Status
This project is still under active development.
More features will be added such as:
- Authentication and Authorization
- RESTful Web API version
- Logging and Exception Handling
- Unit Testing
- UI Enhancements
🙋‍♂️ Author

Ahmed Saleh
GitHub: https://github.com/ahmedsaleh374
LinkedIn: www.linkedin.com/in/ahmedsaleh8090
Email: ahmedsaleh50047@gmail.com
⭐ Support
If you find this project helpful, feel free to star ⭐ the repo or follow me for more updates!
