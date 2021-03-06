# ASPDotNetCoreWebAPI

This project demonstrates the use of ASP .NET Core RESTful API and ORM Entity Framework (EF) Core (Code First) whose aim is to store user information.

## Requirements are outlined below:

### 1. The API should expose the following endpoints:

####	POST /api/users

Create a user with the following attributes:

- `Email` 
- `Password`
- `DisplayName` 

All fields are mandatory, with password be at least 8 characters, must only accept with at least 1 upper case and 1 numeric. 

#### PUT /api/users/{userID}

Update the specified user.

Only accepts update on DisplayName and password.

Password must meet the requirement as stated above.

#### GET /api/users

List all users, excluding password.

Be able to sort and filter by email.

Support paging (default page size = 10)

#### DELETE /api/users/{userID}

Delete the specified user.

### 2. Unit testing should be included.

Code coverage is not that important. This is just to demonstrate an understanding of unit testing an application with a DB dependency.

### 3. Use any database, including InMemory database.

### 4. Use any ORM, but EFCore code first would be preferred.

## Nice to have:

### a. Show a clear separation between controller and business logic. 

### b. Domain-driven design.

### c. GET /api/users can be sorted/filtered by any columns using a generic way.

