# Discount service

This is a quick example of how to set up a discount service. It's based on dotnet core 6 and Entity Framework (with a sqlite backend) and it uses Swagger for API documentation.

## Assumptions

I assume that the kind of discount codes that the store want to create are individual codes and not general codes (eg same code for multiple customers).
I assume that the load from the store owners when creating the discount codes will be limited, and the heavy load will be from the users requesting discount codes. One way of solving this would be to do a database route that routes write and read to different databases (kinda like how it's possible in Django). This way you can still use a regular SQL database without much trouble. But if both writes and reads is going up it could easily be swapped for some kind of NoSQL-database that is better at scaling since BASE is easier to scale than ACID.

## Initial idea (and maybe proper) to solve this problem

My intial idea was to build a micro service architecture based on Azure Functions. Each endpoint would be an Azure Function responsible for one task and one task only, creating and retrieving discount codes. But due to difficulties with getting Azure credentials for the specific functions I had to scrap this idea. If I had more time to making this as a proper product this would be the way to go.
One way of optmizing this idea would be to write some simple Rust functions and deploy it to AWS Lambda.

## How to get started

* Clone repo and run
* `dotnet restore`
* `dotnet ef database update`
* `dotnet run`

## To test the endpoints

* `dotnet run` should have fired up a browser, head over to /swagger/ and you got an glance of the API.
* There is already a store created with the ID of `3fa85f64-5717-4562-b3fc-2c963f66afa6`
* Create some new discount codes with the post endpoint, fill in how many discoount codes you want
* Use the get endpoint to retrieve the discount code (no matter what I cant get Swagger/Swashbuckle to fill in a GUID in the UI, so paste this in StoreId: `3fa85f64-5717-4562-b3fc-2c963f66afa6`)
* Run the get endpoint more than the number of codes you created and you will end up with a 404

## Thoughts and architectural considerations

This is a very small service and if it would have been for real I would have modeled it a bit different. One thing is that the store owners probably want to create campaigns and follow how they are used. So if you create 100 codes an email campaign for certain users it would be good to keep track of how well that campaign fans out.
One thing that could be interesting could be to able to create general discount codes that can be used the mutliple people. Here it would also be good with a good analytics tool to see the progress. And for those kind of campaigns you also need to beale to deactivate them.
My intial idea would have been really sweet to implement, but due to Azure giving me a hard time that idea have to wait a bit =)