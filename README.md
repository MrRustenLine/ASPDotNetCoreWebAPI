# ASPDotNetCoreWebAPI
This project demonstrates the use of ASP .NET Core RESTful API and ORM Entity Framework (EF) Core (Code First).

Its aim is to store user information using .NET Core 6.

Requirements are outlined below:

# The API should expose the following endpoints:

##	POST /api/users
### 	Create a user with the following attributes:
####•	Email
•	Password
•	DisplayName
###	All fields are mandatory, with password be at least 8 characters, must only accept with at least 1 upper case and 1 numeric. 

## PUT /api/users/{userID}
	Update the specified user.
	Only accepts update on DisplayName and password.
	Password must meet the requirement as stated above.

## GET /api/users
	List all users, excluding password.
	Be able to sort and filter by email (Bonus point: great if can be sorted/filtered by any columns using a generic way)
	Support paging (default page size = 10)
Show a clear separation between controller and business logic. 

## DELETE /api/users/{userID}
	Delete the specified user.

# Unit testing should be included.

Code coverage is not that important. This is just to demonstrate an understanding of unit testing an application with a DB dependency.

# Demonstrate understanding domain-driven design.

# Use any database, including InMemory database.

# Use any ORM, but EFCore code first would be preferred.


