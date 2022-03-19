# Discount service

This is a quick example of how to set up a discount service. It's based on dotnet core 6 and Entity Framework (with a sqlite backend) and it uses Swagger for API documentation.

## Assumptions

I assume that the kind of discount codes that the store want to create are individual codes and not general codes (eg same code for multiple customers).
I assume that the load from the store owners when creating the discount codes will be limited, and the heavy load will be from the users requesting discount codes.

## Initial idea and (maybe proper) to solve this problem

My intial idea was to build a micro service architecture based on Azure Functions (or AWS Lambda). Each endpoint would be an Azure Function responsible for one task and one task only, creating and retrieving discount codes. But due to difficulties with getting Azure credentials for the specific functions I had to scrap this idea. If I had more time to making this as a proper product this would be the way to go.

## How to get started

* Clone repo and run
* `cd DiscountService`
* `dotnet restore`
* `dotnet ef database update`
* `dotnet watch run`

or in one row:
* `cd DiscountService && dotnet restore && dotnet ef database update && dotnet watch run`

## To test the endpoints

* `dotnet watch run` should have fired up a browser, head over to `/swagger/` and you got an glance of the API.
* There is already a store created with the ID of `3fa85f64-5717-4562-b3fc-2c963f66afa6`
* Create some new discount codes with the post endpoint, fill in how many discount codes you want
* Use the get endpoint to retrieve the discount code (no matter what I cant get Swagger/Swashbuckle to fill in a GUID in the UI, so paste this in StoreId: `3fa85f64-5717-4562-b3fc-2c963f66afa6`)
* Run the get endpoint more than the number of codes you created and you will end up with a 404

## Thoughts and architectural considerations

This is a very small service and if it would have been for real I would have modeled it a bit different. One thing is that the store owners probably want to create campaigns and follow how they are used. So if you create 100 codes an email campaign for certain users it would be good to keep track of how well that campaign pans out.
One thing that could be interesting could be to able to create general discount codes that can be used by multiple people. Here it would also be good with a good analytics tool to see the progress. And for those kind of campaigns you also need to able to deactivate them.

### APIs

This is a very simple system and I see very little advantages of using something other than a regular REST approach. It's a limited set of services (and maybe just our own) that will use the service so we know the use cases that they have and can model the DTOs to fit our own needs. REST is also very easy to cache (if needed).

### Authentication (and authorization)

The code sample has no authentication what so ever but could be easily implemented. Going with JWT route can be tempting at first but can bring several downsides along the way. If there is a neew for other microservices to authenticate I would suggest to build a central authentication service, maybe with Redis as base for easy usage and high performance.

### Async

The code as it is today are totally synchronous and that won't scale. One easy way of making it async (and scale) is to offload the creation of discount codes part to a worker, so the post API would just create a new message in a broker queue (like RabbitMQ etc) and then be done. This also ties nicely together with my initial idea about using Azure Functions, and if you are into running Kubernetes it's also possible to run Azure Functions there as well. And from a cost perspective Azure Functions is a sweet spot.
One way of optmizing this even furhter would be to write some simple Rust functions and deploy it to AWS Lambda. [A recent paper](https://torbjornzetterlund.com/how-environmental-friendly-is-programming-languages/) has shown that Rust has almost the same speed as C, with C# a bit behind and Python waaay behind so both from a cost perspective but also from an environmental perspective it makes sense. Quicker execution also means that each worker can do more work in the same time.

### Data validation

I do no validation what so ever (well, if you don't throwing some 404 validation). One point to take into consideration here is if we need an API gateway (which we probably do), if so then many of the regular validations (parameter data type etc) can be offloaded to the gateway so the microservice don't even have to be hit on an invalid request. Of course the microservice still has to validate every input and data that we read from the database.

### Scalability and data store

Regarding the scalability I'm thinking about using a database route that routes write and read to different databases (kinda like how it's possible in Django). This way you can still use a regular SQL database without much trouble. But if both writes and reads are going up it could easily be swapped for some kind of NoSQL-database that is better at scaling since BASE is easier to scale than ACID. There are also SQL databases that scale really well nowadays, like CockroachDB etc.

### Testing and ease of future management

One good thing about choosing Dotnet 6 is that it's much easier to do testing due to the removal of the `Startup.cs`, but writing tests is something I didn't have time to do.

My intial idea with Azure Functions would have been really sweet to implement, but due to Azure giving me a hard time that idea have to wait a bit =)

