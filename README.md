Boat and Trip Booking System



Features

    User Roles: Admin, Owner, and Customer, each with role-based permissions.
    Authentication: Secure JWT-based authentication and authorization.
    Boat Management: Owners can register boats, which must be approved by Admins before they become available for booking.
    Trip Management: Owners can create trips with detailed descriptions, prices, and additional services.
    Wallet System: Customers and Owners can manage wallets for payments and refunds.
    Booking System: Customers can book trips or boats, select services, and make payments through the platform.
    Logging: Serilog integration for tracking user actions and system events.
    Error Handling: Comprehensive error handling to ensure robustness and user-friendly feedback.

Table of Contents

    Technologies Used
    Project Structure
    Installation
    Usage
    Authentication
    API Endpoints
    Logging and Error Handling
    Database Schema


    Here’s a professional README.md for the project repository, formatted for GitHub:
Boat and Trip Booking System
Overview

The Boat and Trip Booking System is a web-based platform that allows users to manage and book boats and trips. This project is built using .NET 8 and follows modern software architecture principles, including CQRS, repository pattern, and JWT-based authentication. It is designed for three types of users: Admin, Owner, and Customer, each with specific functionalities within the system.
Features

    User Roles: Admin, Owner, and Customer, each with role-based permissions.
    Authentication: Secure JWT-based authentication and authorization.
    Boat Management: Owners can register boats, which must be approved by Admins before they become available for booking.
    Trip Management: Owners can create trips with detailed descriptions, prices, and additional services.
    Wallet System: Customers and Owners can manage wallets for payments and refunds.
    Booking System: Customers can book trips or boats, select services, and make payments through the platform.
    Logging: Serilog integration for tracking user actions and system events.
    Error Handling: Comprehensive error handling to ensure robustness and user-friendly feedback.

Table of Contents

    Technologies Used
    Project Structure
    Installation
    Usage
    Authentication
    API Endpoints
    Logging and Error Handling
    Database Schema
    Contributing

Technologies Used

    .NET 8: For backend development.
    Entity Framework Core: For data access and database management.
    SQL Server: For storing all data.
    JWT: For secure authentication and authorization.
    Serilog: For logging system activities and errors.
    Signalr : To send Notifications
    Swagger: For API documentation.
    Cqrs and Metidatr : to seprate read and write and mediatr manage the request 
    Repository pattern and UnitOfWork : To seprate the bussinies logic than data access layer 
    Speceifcation Pattern
    Factory Pattern 



Project Structure

graphql

src/
|-- AppSquareTask.Application/       # Application logic, services, and DTOs
|-- AppSquareTask.Core/              # Core business logic and domain models
|-- AppSquareTask.Infrastructure/    # Data access layer and repositories
|-- AppSquareTask.WebApi/            # API controllers and configuration



Installation
Prerequisites

    .NET 8 SDK
    SQL Server
    Visual Studio or Visual Studio Code

Steps

    Clone the Repository:

    bash

git clone [https://github.com/your-username/boat-trip-booking-system](https://github.com/chixes404/AppSquareTask/).git


Setup Database:

    Update the connection string in appsettings.json in the AppSquareTask.WebApi project.
    Apply migrations:

    bash

    dotnet ef database update

Run the Application:

bash

dotnet run --project AppSquareTask.WebApi




Database Schema

The application uses Entity Framework Core with a SQL Server database.

To initialize the database, run the following migrations:

bash

dotnet ef migrations add InitialCreate
dotnet ef database update
