# Awesome Music

Development code test for a previous job application.

API url => https://awesomemusicapi.azurewebsites.net/swagger/index.html

**_NOTE:_** Not working anymore since I shutdown the Azure App Service for it.

## Requirements

- [NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com) (optional)

## To run

Just open the solution and run it from Visual Studio.

If you want to run it manually go to /AwesomeMusic/AwesomeMusic and run:
```bash
$ dotnet run
```
It will give you something like this:

![run](https://github.com/jestra52/AwesomeMusic/blob/master/run.PNG)

Then, open in your browser https://localhost:5001/swagger

## Authenticate

Make a request to /User/Authenticate:

![auth-body](https://github.com/jestra52/AwesomeMusic/blob/master/auth-body.PNG)

And it will give you an answer with the authentication token:

![token](https://github.com/jestra52/AwesomeMusic/blob/master/token.PNG)

Then copy the token, go to the top of the page and click in Authorize. Then put the token like this:

Bearer \<token\>

![authorize](https://github.com/jestra52/AwesomeMusic/blob/master/authorize.PNG)

And you're done! You can now make requests to private endpoints.

