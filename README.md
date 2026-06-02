# BistroPro - Restaurant Management System

## Overview

BistroPro is a web-based Restaurant Management System designed to help restaurant managers efficiently handle reservations, table assignments, and daily restaurant operations.

The system provides a modern and responsive dashboard that simplifies reservation management while maintaining a clean and user-friendly interface.

---

## Features

### Reservation Management

* View all reservations in a centralized dashboard.
* Create new reservations.
* Update existing reservations.
* Cancel reservations.
* Track reservation status (Confirmed, Pending, Cancelled).

### Table Management

* Manage restaurant tables.
* Assign tables to reservations.
* Monitor table availability.

### Dashboard Statistics

* View today's reservation count.
* View pending reservation requests.
* Monitor reservation activity in real-time.

### Search & Filtering

* Search reservations by customer information.
* Filter reservations by status.
* Filter reservations by reservation date.

### Quick Actions

* Auto-Assign Tables.
* Send SMS Alerts to customers.

---

## Technologies Used

### Backend

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* LINQ

### Frontend

* Razor Views
* HTML5
* Tailwind CSS
* JavaScript

### Database

* SQL Server

---

## System Architecture

The project follows a layered architecture:

* Presentation Layer (MVC Views)
* Business Logic Layer (Services)
* Data Access Layer (Entity Framework Core)
* SQL Server Database

This structure improves maintainability, scalability, and code organization.

---

## Database Entities

### Customer

* Id
* Name
* PhoneNumber

### Table

* Id
* TableNumber
* Capacity
* Status

### Reservation

* Id
* CustomerId
* TableId
* ReservationDate
* NumberOfGuests
* Status

---

## Reservation Status

The system supports the following reservation statuses:

* Pending
* Confirmed
* Cancelled

---

## Future Enhancements

* Authentication & Authorization
* Role Management
* Pagination
* AJAX Filtering
* Real-Time Updates using SignalR
* Reporting & Analytics
* Email Notifications
* Online Reservation Portal

---

## Getting Started

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio 2022

### Installation

1. Clone the repository.

```bash
git clone https://github.com/yourusername/BistroPro.git
```

2. Navigate to the project directory.

```bash
cd BistroPro
```

3. Update the connection string in:

```json
appsettings.json
```

4. Apply database migrations.

```bash
dotnet ef database update
```

5. Run the application.

```bash
dotnet run
```

---

## Author

Abdelrahman Mohammed

ASP.NET Core Backend Developer
