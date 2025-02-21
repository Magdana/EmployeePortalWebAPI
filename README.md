
# EmployeePortalWebAPI

EmployeePortalWebAPI is a .NET 6-based Web API for managing employee-related operations. It includes authentication and role-based authorization using JWT, repositories, and services. The API supports three roles: Admin, Manager, and User, with specific access restrictions.


## Technologies Used

- .NET 6

- ASP.NET Core Web API

- Entity Framework Core

- SQL Server

- JWT Authentication

- Repository & Service Pattern
## Features

- User Authentication: Secure login and token generation using JWT.
- Role-Based Authorization: Admin, Manager, and User roles with restricted access.
- Employee Management: CRUD operations on employees.
- Company Management: CRUD operations on companies.
- Epic Management: CRUD operations on epics.


## Installation
Prerequisites

- .NET 6 SDK
- SQL Server
- Visual Studio / VS Code

Setup

- Clone the repository:

```bash
  git clone https://github.com/Magdana/EmployeePortalWebAPI
  cd EmployeePortalWebAPI
```
- Update appsettings.json with your database connection string.
- Apply migrations and update the database:
```bash
  dotnet ef database update
```
- Run the API:
```bash
  dotnet run
```


    
## API Reference

#### User registration

```http
  POST /api/Authorization/register
```
#### Authenticate and get a JWT token.

```http
  POST /api/Authorization/login
```

#### Get companies list (Admin, Manager only)

```http
  GET /api/Company/GetAllCompaies
```

#### Get company by id (Admin, Manager only)

```http
  GET /api/Company/GetCompanyById/${id}
```

#### Get company with employees (Admin only)

```http
  GET /api/Company/GetCompaniyWithEmployee/${id}
```

#### Add company (Admin only)

```http
  POST /api/Company/AddCompany
```

#### edit company (Admin only)

```http
  PUT /api/Company/UpdateCompany
```

#### delete company (Admin only)

```http
  DELETE /api/Company/DeleteCompany/${id}
```

#### Get employees list (Admin, Manager, User)

```http
  GET /api/Employee/GetAllEmployees
```

#### Get top ten earliest employees list (Admin, Manager only)

```http
  GET /api/Employee/GetTopTenEarliestEmployees
```

#### Get top high salary employees list (Admin, Manager only)

```http
  GET /api/Employee/GetTopHigjSalaryEmployees
```

#### Get soft deleted employees list (Admin, Manager only)

```http
  GET /api/Employee/GetSoftDeletedEmployees
```

#### Get employee by id (Admin, Manager, User)

```http
  GET /api/Employee/GetEmployeeById/${id}
```

#### Add employee (Admin, Manager only)

```http
  POST /api/Employee/AddEmployee
```

#### edit employee (Admin, Manager only)

```http
  PUT /api/Employee/UpdateEmployee
```

#### delete employee (Admin, Manager only)

```http
  DELETE /api/Employee/DeleteEmployee/${id}
```

#### Get epics list (Admin, Manager only)

```http
  GET /api/Epic/GetAllEpics
```

#### Get epics by id (Admin, Manager, User)

```http
  GET /api/Epic/GetEpicById/${id}
```

#### delete epic (Admin, Manager only)

```http
  DELETE /api/Epic/DeleteEpic
```

#### add epic (Admin, Manager only)

```http
  POST /api/Epic/AddEpic
```

#### update epic (Admin, Manager only)

```http
  PUT /api/Epic/UpdateEpic
```

#### get epics by company (Admin, Manager only)

```http
  GET /api/Epic/GetEpicsByCompany
```

#### get epics by employee (Admin, Manager, User)

```http
  GET /api/Epic/GetEpicsByEmployee
```

#### get epics by status (Admin, Manager only)

```http
  GET /api/Epic/GetEpicsByStatus
```

#### get all users (Admin, Manager, User)

```http
  GET /api/User/GetAllUsers
```

#### get user info (Authorized user info)

```http
  GET /api/User/GetMyInfo
```

#### delete user (Admin, Manager only)

```http
  DELETE /api/User/DeleteUser
```

#### Update user (Admin only)

```http
  PUT /api/User/UpdateUserRole
```
## Role-Based Access

- Admin: Full access to all endpoints.

- Manager: Read and edit access to employees, companies and epics .

- User: Limited access, based on business rules.
## License

[MIT](https://choosealicense.com/licenses/mit/)


## Authors

- [@Magda Gvirjishvili](https://github.com/Magdana)

