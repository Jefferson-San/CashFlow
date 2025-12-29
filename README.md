## 📌 CashFlow — Expense Management System

A complete project for **expense management**, developed with **Clean Architecture**, **SOLID principles**, **CQRS-lite + MediatR**, **Docker**, **CI/CD**, **caching**, and **total focus on scalability**.

**Objective:** To develop a solid foundation for modern applications by applying good architectural practices, design patterns, and techniques that promote clean, decoupled, testable, and scalable code.

---


# 🚀 Technologies Used

- **ASP.NET Core 8**
- **C# 12**
- **Entity Framework Core**
- **MediatR (CQRS)**
- **AutoMapper**
- **Flunt (Notification Pattern)**
- **PostgreSQL**
- **Docker**
- **Migrations EF Core**
- (under construction) **Automated testing with xUnit**
- (under construction) **Caching with Redis**
- (under construction) **Authentication / Authorization**
- (Under construction) **Integration with Grafana and Prometheus for metrics and observability**

# Architecture

The project follows the principles of Clean Architecture, divided into layers

- 📁 CashFlow.Api             → Endpoints / Controllers / Swagger
- 📁 CashFlow.Application     → Use Cases, CQRS-lite (Commands/Queries), Validators
- 📁 CashFlow.Domain          → Entities, Interfaces, Models, Domain Rules
- 📁 CashFlow.Infrastructure  → Persistence (EF Core), Repositories, Migrations, Redis
- 📁 CashFlow.Tests           → Unit & Integration Tests
