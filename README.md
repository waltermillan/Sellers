# 🧑‍💼 Sellers Project

Visualization of sellers. Classic CRUD system.

---

## 📅 Changelog

- **14/10/2026**: Database: migrated the database scripts from MariaDB to Oracle in the `migration/oracle` branch. Added DDL scripts for table creation, sequences, and triggers; DML scripts for initial data; DQL validation queries; and an updated Entity-Relationship Diagram (ERD) based on the Oracle schema.

---

## 🎯 Objective

Practice development with .NET (C#), SQL, and Angular (TypeScript). This branch focuses on migrating the database from MariaDB to Oracle.

Technologies:

- **.NET (C#)**
- **Oracle Database**
- **Angular (TypeScript)**
- **Design Patterns**
- **Onion Architecture**

---

## 🚀 Features

### 🔧 Backend

- Based on **Onion Architecture**
- Implements design patterns, including Data Transfer Object (DTO).
- **Key Libraries**:
  - **Encryption**: `System.Security.Cryptography` (AES-256)
  - **Logging**: `Serilog`, `Serilog.Extensions.Logging`, `Serilog.Sinks.File`
  - **ORM**: Entity Framework Core (Oracle provider, if configured)

---

### 💻 Frontend

- Built with **Angular 18.2.14**
- Features:
  - Reactive Forms
  - AuthService and HTTP Interceptors
  - Custom Pipes and Shared Modules

---

### 🗄️ Database

- Uses **Oracle Database**
- Includes:
  - Entity-Relationship Diagram (ERD)
  - DDL scripts for table creation
  - Sequence creation scripts
  - Trigger creation scripts
  - DML scripts for initial data insertion
  - DQL scripts for validation queries

---

## 🧪 Installation

### ✅ Prerequisites

- [.NET SDK 9.0.200](https://dotnet.microsoft.com/)
- Oracle Database
- [Node.js + npm](https://nodejs.org/) (for frontend)
- [Postman 11.44.3](https://www.postman.com/downloads/)

---

### 🔧 Setup Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/waltermillan/Sellers.git
   ```

2. Switch to the Oracle migration branch:
   ```bash
   git checkout migration/oracle
   ```

3. Execute the SQL scripts in this order:
   - `01_DDLs_CREATE_TABLES.sql`
   - `02_DDLs_CREATE_SEQUENCES.sql`
   - `03_DDLs_CREATE_TRIGGERS.sql`
   - `04_DMLs_INSERT_INITIAL_DATA.sql`
   - `05_DQLs_VALIDATION_QUERIES.sql`

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
