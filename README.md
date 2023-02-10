# Take Home assignment from EBANX

.NET 6.0 API developed following https://ipkiss.pragmazero.com/ requirements.

The API is structured according to practices and principles such as Clean Code,
Clean Architecture, DDD, TDD, and SOLID.

The data is persisted to an in-memory database.

## Install and Run

Install [.NET 6.0](https://dotnet.microsoft.com/en-us/download), clone this
repository and run the following command from the root path.

```sh
dotnet run --project src/Web/
```

Accessing `/swagger` will redirect you to the API documentation.

Use the following command from the root path to run tests.

```sh
dotnet test
```
