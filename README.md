# 🧑‍💼 Sellers Project

Visualization of sellers. Classic CRUD system.

---

## 📅 Changelog

- **03/04/2025**: Unit tests added to Angular components. Backend: added endpoint `http://localhost:9876` to prevent test failures.
- **08/03/2025**: Backend/Frontend: Cleaned up code and removed unnecessary comments. Database: ER diagram added.
- **07/03/2025**: Added `HashPassword` class for encrypting login passwords. General code/comment cleanup.
- **27/02/2025**: Added unit tests using xUnit and Mock libraries. Fixed URLs to comply with RESTful specifications. Modified login URL and resolved multiple bugs.
- **26/02/2025**: Backend: API adjustments to follow RESTful conventions. Frontend: URL invocation fixes and added Use module.
- **24/02/2025**: The system evolved into a full product sales management platform. Added entities: Buyer, Product, Sale, User, and Login.
- **14/02/2025**: Initial commit. Added Backend, Frontend, and Database. Implemented data export to Excel/PDF. Improved data presentation.

---

## 🎯 Objective

Practice development with .NET (C#), SQL, and Angular (TypeScript) — initially without using design patterns or Onion Architecture. Connects to MariaDB via Entity Framework, with Docker and DBeaver support.

Technologies:

- **.NET (C#)** and **SQL Server**
- **Angular (TypeScript)**
- **Design Patterns**
- **Onion Architecture**

---

## 🚀 Features

### 🔧 Backend

- Based on **Onion Architecture**
- Implements several **Design Patterns**, including:
  - Data Transfer Object (DTO)
- **Key Libraries**:
  - **Encryption**:
    - `System.Security.Cryptography` (AES-256)
  - **Logging**:
    - `Serilog`
    - `Serilog.Extensions.Logging`
    - `Serilog.Sinks.File`
  - **ORM**:
    - `Pomelo.EntityFrameworkCore.MySql` (MariaDB provider)

---

### 💻 Frontend

- Built with **Angular 18.2.14**
- Features:
  - Reactive Forms
  - AuthService and HTTP Interceptors
  - Custom Pipes and Shared Modules

---

### 🗄️ Database

- Uses **MariaDB**, running via **Docker Desktop**
- Includes:
  - Entity-Relationship Diagram (ERD)
  - Sample data insertion scripts (`.sql`)
  - **DDL scripts** for table creation
  - **DML scripts** for sample data insertion

---

## 🧪 Installation

### ✅ Prerequisites

Ensure the following tools are installed:

- [.NET SDK 9.0.200](https://dotnet.microsoft.com/)
- [Docker Desktop 4.40.0+](https://www.docker.com/)
- [Node.js + npm](https://nodejs.org/) (for frontend)
- [Postman 11.44.3](https://www.postman.com/downloads/)

---

### 🔧 Setup Steps

1. Clone the repository:
    ```bash
    git clone https://github.com/waltermillan/Sellers.git
    ```

2. Follow the video guide for setup:
    - [Version 1 - Display Version](https://youtu.be/Qhcey6a2Xe0)

3. Complete any remaining setup steps described in the project documentation.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
