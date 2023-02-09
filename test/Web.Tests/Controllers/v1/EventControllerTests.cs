using System;
using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Deposit.Commands;
using Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;
using Ebanx.Services.Account.Domain.Account;
using Ebanx.Services.Account.Domain.Transaction;
using Ebanx.Services.Account.Domain.Transaction.Entities;
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

    private async Task Event_Mock(Transaction mediatorSetupResult, Type controllerExpectedResponse)
    {
        //Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), default))
            .ReturnsAsync(mediatorSetupResult);

        //Act
        var result = await _eventController.Event(new CreateTransactionRequest(default, default, default, default));

        //Assert
        Assert.IsType(controllerExpectedResponse, result);
        _mediatorMock
            .Verify(m => m.Send(It.IsAny<CreateTransactionCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task GetBalance_ShouldReturnOk_WhenAccountExists()
    {
        await Event_Mock(
            new Transaction(default, default,default, default),
            typeof(OkResult));
    }

    [Fact]
    public async Task Event_ShouldReturnNotFound_WhenTransactionIsNotPossible()
    {
        await Event_Mock(
            default,
            typeof(NotFoundResult));
    }
}