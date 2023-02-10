using System;
using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;
using Ebanx.Services.Account.Domain.Transaction;
using Ebanx.Services.Account.Web.Contracts.Transaction;
using Ebanx.Services.Account.Web.Controllers.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Web.Tests.Controllers.v1;

public class EventControllerTests
{
    private readonly EventController _eventController;
    private readonly Mock<IMediator> _mediatorMock;

    public EventControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _eventController = new EventController(_mediatorMock.Object);
    }

    private async Task EventTest(ITransaction? mediatorSetupResult, (Type StatusCode, object Body) controllerExpectedResponse)
    {
        //Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), default))
            .ReturnsAsync(mediatorSetupResult);

        //Act
        var result = (await _eventController.Event(new CreateTransactionRequest())).Result as ObjectResult;

        //Assert
        Assert.IsType(controllerExpectedResponse.StatusCode, result);
        Assert.Equal(controllerExpectedResponse.Body, result?.Value);
        _mediatorMock
            .Verify(m => m.Send(It.IsAny<CreateTransactionCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task Event_ShouldReturnOk_WhenTransactionIsPossible()
    {
        (Type StatusCode, ITransaction Body) controllerExpectedResponse = (typeof(CreatedResult),  new Mock<ITransaction>().Object);
        await EventTest(controllerExpectedResponse.Body, controllerExpectedResponse);
    }

    [Fact]
    public async Task Event_ShouldReturnNotFound_WhenTransactionIsNotPossible()
    {
        var controllerExpectedResponse = (typeof(NotFoundObjectResult), 0);
        await EventTest(default, controllerExpectedResponse);
    }
}