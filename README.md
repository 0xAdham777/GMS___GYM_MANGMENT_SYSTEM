🏋️‍♂️ GYM Management System 📌 Overview

GYM Management System (GMS) is a complete Windows Forms desktop application built with C# .NET Framework and SQL Server. The system helps gym owners and staff manage all essential operations including members, subscriptions, payments, attendance, and training sessions — all from a modern, user-friendly interface.

✨ Features

🔹 People Management – Add, update, view, and delete personal information including full name, phone number, email, gender, blood type, address, and profile photo.

🔹 Coaches Management – Manage coaches, link them with their personal info and specializations, with filtering and search support.

🔹 Members Management – Track members, subscriptions, debts, notes, status (Active/Inactive), and remaining days.

🔹 Subscription Types – Define and manage subscription packages (Price, Duration, Description).

🔹 Subscriptions – Register subscriptions for members, set start and end dates, and link to subscription types.

🔹 Payments – Manage payments (Cash / Card), view payment history, and calculate total revenues.

🔹 Attendance – Register member check-ins and check-outs with time and notes.

🔹 Training Sessions – Assign training sessions to coaches and members, including schedule and notes.

🔹 User Management – Create and manage system users with login credentials and active status.

🔹 Database Backup – Create a full backup of the system database directly from the application.

🛠️ Tech Stack

C# .NET Framework (WinForms)

SQL Server 2019+

ADO.NET (Data Access Layer)

Guna.UI2 Framework for modern UI components

Layered Architecture: Data Access Layer (DAL) → Business Layer → Presentation Layer (UI)

📊 Database Schema

The system database is normalized and includes these main entities:

Persones (people data)

Members (gym members)

Coaches (linked with specializations)

Coach Specializations

Users (system accounts)

Subscriptions (linked to members and types)

Subscription Types (packages with price and duration)

Payments (linked to subscriptions and members)

Attendance (member check-in/out)

Training Sessions (coach + member assignments)

Bloods (blood types)

🚀 Getting Started

Open the project in Visual Studio.

Import the database script GYM_ManagementSystem.sql into SQL Server.

Update the ConnectionString in App.config with your SQL Server settings.

Run the project 🎉.

👨‍💻 Author

Developed by Adham Bennour 📧 bennouradham2@gmail.com
