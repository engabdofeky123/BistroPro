# BistroPro

A modern Restaurant Management System built using ASP.NET Core MVC and Entity Framework Core.

The system helps restaurant administrators manage tables, reservations, meals, customer orders, and daily operations through a clean and user-friendly dashboard.

## Features

### Authentication & Authorization

* Secure login and logout functionality
* Role-based authorization
* Protected admin pages

### Dashboard

* Restaurant overview
* Total reservations statistics
* Total guests statistics
* Occupancy percentage tracking

### Table Management

* View all restaurant tables
* Add new tables
* Update table availability status
* Manage seating capacity

### Reservation Management

* Create reservations
* View reservation schedule
* Reservation status tracking
* Assign reservations to tables

### Meal Management

* Add new meals
* Edit meal details
* Delete meals
* Manage meal availability

### Order Management

* Create customer orders
* Manage order items
* Track order status
* Calculate total order amount

## Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* C#
* Razor Views
* Bootstrap / Tailwind CSS
* LINQ

## Database Entities

* Customers
* Meals
* Orders
* OrderItems
* Reservations
* RestaurantTables
* ApplicationUsers

## Project Structure

```text
Controllers/
Models/
ViewModels/
Views/
Data/
Repositories/
Services/
Identity/
```

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/your-username/Restaurant-Management-System.git
```

### Update Connection String

Open:

```json
appsettings.json
```

Update the SQL Server connection string.

### Apply Migrations

```powershell
Update-Database
```

### Run the Project

```powershell
dotnet run
```

## Future Improvements

* SMS Notifications
* Automatic Table Assignment
* Daily Reports Export
* Reservation Analytics
* Email Notifications
* PDF Report Generation

## Author

Abdelrahman Mohammed

ASP.NET Core Backend Developer
