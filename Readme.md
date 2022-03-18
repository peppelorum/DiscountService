# Discount service

This is a quick example of how to set up a discount service. It's based on dotnet core 6 and Entity Framework (with a sqlite backend) and it uses Swagger for API documentation.

## Assumptions

I assume that the kind of discount codes that the store want to create are general discount codes and not personalised discount codes.
I assume that the load from the store owners when creating the discount codes will be limited, and the heavy load will be from the users requesting discount codes. One way of solving this would be to do a database route that routes write and read to different databases (kinda like how it's possible in Django). This way you can still use a regular SQL database without much trouble. But if both writes and reads is going up it could easily be swapped for some kind of NoSQL-database that is better at scaling since BASE is easier to scale than ACID.

## Initial idea (and maybe proper) to solve this problem

My intial idea was to build a micro service architecture based on Azure Functions. Each endpoint would be an Azure Function responsible for one task and one task only, creating and retrieving discount codes.

## How to get started

* Clone repo and run
* `dotnet restore`
* `dotnet ef database update`
* `dotnet run`