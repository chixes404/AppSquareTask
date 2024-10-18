# Boat and Trip Booking System

## Features

* User Roles: Admin, Owner, and Customer, each with role-based permissions.
* Authentication: Secure JWT-based authentication and authorization.
* Boat Management: Owners can register boats, which must be approved by 
* Admins before they become available for booking.
* Trip Management: Owners can create trips with detailed descriptions, prices, and additional services.
* Wallet System: Customers and Owners can manage wallets for payments and refunds.
* Booking System: Customers can book trips or boats, select services, and make payments through the platform.
* Logging: Serilog integration for tracking user actions and system events.
* Error Handling: Comprehensive error handling to ensure robustness and user-friendly feedback.




## Technology i used
* .NET 8: For backend development.
* Entity Framework Core: For data access and database management.
* SQL Server: For storing all data.
* JWT: For secure authentication and authorization.
* Fleunt Validation : allows for clear and concise validation rules while keeping the logic clean and separated from the core domain models.
* Serilog: For logging system activities and errors.
* Signalr : To send Notifications
* Swagger: For API documentation.
* Cqrs and Metidatr : to seprate read and write and mediatr manage the request 
* Repository pattern and UnitOfWork : To seprate the bussinies logic than data access layer 
* Speceifcation Pattern : design pattern used to encapsulate complex business logic into reusable and combinable rules. In this project, it helps to keep the business logic clean and testable by allowing the definition of criteria that can be reused across multiple entities.

### Project Strucure




```bash
 |-- AppSquareTask.Application/ # Application logic, services, and DTOs
 |-- AppSquareTask.Core/ # Core business logic and domain models
 |-- AppSquareTask.Infrastructure/ # Data access layer and repositories 
 |-- AppSquareTask.WebApi/ # API controllers and configuration
```

## Install Prerequisites



```bash
.NET 8 SDK
SQL Server
Visual Studio or Visual Studio Code
```

## Usage Step

Clone the Repository:

```bash
git clone https://github.com/chixes404/AppSquareTask.git

Update the connection string in appsettings.json in the AppSquareTask.WebApi project.
Apply migrations:

dotnet ef database update

