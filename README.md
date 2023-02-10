# Take Home assignment from EBANX

## About

The application is a .NET 6.0 API developed following https://ipkiss.pragmazero.com/ requirements.

### Development and architecture

- The API is structured according to practices and principles such as Clean Code,
  Clean Architecture, DDD, TDD, and SOLID.
- It was developed following TBD (trunk based development) and PRs (pull requests) for every new code
  merged to the main branch.
  - The commits were not squashed before merging the feature branch to main.
- Following the Clean Architecture, the projects were divided in 4 layers: Domain, Application, Infrastructure
  and Web. Each one making reference to the next one.
- Unit testing was also created following the test-driven development. Using the dependency inversion enabled
  the use of mocks.

### API and domain

- The driven-domain design was another approach as well. The `/event` endpoint became a transaction within
  the application which can be a deposit, a withdraw or a transfer.
  - This approach relies on Single Responsibility from SOLID principles.
  - The endpoint `/balance` was translated to operations with an `Account` domain model which owns a balance
    property. This model also had the operations to create withdraws and deposits within the domain.

### Other approaches

- The Mediator design pattern was used to decouple and enable the communication between the Web layer and the
  Application one.
  - Because of the .NET framework and Mediatr library, it was possible to reassert the use of Dependency Injection.
    - This approach also relies on Dependency Inversion from SOLID principles.
  - Among the mediator behavior, its pipeline was used to attach a validator for each request created.
- The data is persisted to an in-memory database. Once the application is down, the data is lost.
  - The mediator approach also ensured CQRS, distinguishing the requests between command or query.
  - The CQRS approach was used, using interfaces required from the Application layer which were implemented
    by the Infrastructure layer. There was one for reading and another for writing operations
    - This approach also relies on Integration Segregation from SOLID principles.

### Example

The following section explains the flow of a deposit operation.

1. Calling `POST /event {"type":"deposit", "destination":"100", "amount":10}` would reach the `/event` endpoint in the
   Web layer.
2. Inside the controller, it is translated to a `CreateTransactionCommand` handled by a `CreateTransactionCommandHandler`
   in the Application layer.
3. The handler translates the deposit to a `CreateDepositCommand` that is handled by a `CreateDepositCommandHandler`.
4. During the handling process, a validator for each request (command) is also done.
5. The last handler consumes a `IAccountWriter` to do a writing operation with the account in the Infrastructure layer.
6. If the account exists, the deposit amount is added to its balance. Otherwise, the account is created with a balance
   equals to the deposit amount.
7. The response is returned as a deposit domain model.

## Install and Run

You can run the API using either `docker` or `dotnet`.

### Docker

Install [docker](https://docs.docker.com/engine/install/), build the image using.

```sh
docker build . -t account-api
```

And run the application with.

```sh
docker run --network="host" -rm -p 80:80 -p 443:443 account-api
```

- The parameters ensure that the connection can be made through `http://localhost/swagger/index.html`.
- It binds the HTTP and HTTPS ports and your localhost to the container localhost.
- The `-rm` parameter automatically remove the container when it exits.

### Dotnet

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
