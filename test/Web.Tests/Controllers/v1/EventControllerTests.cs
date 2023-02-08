using System;
using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Common;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Web.Controllers.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Web.Tests.Controllers.v1;

public class EventControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly EventController _eventController;
    
    public EventControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _eventController = new EventController(_mediatorMock.Object);
    }

    private async Task Event_Mock(AccountResult mediatorSetupResult, Type controllerExpectedResponse)
    {
        //Arrange
        _mediatorMock
            .Setup(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == mediatorSetupResult.Id), default))
            .ReturnsAsync(mediatorSetupResult);

        //Act
        var result = await _eventController.Event(mediatorSetupResult.Id!);

        //Assert
        Assert.IsType(controllerExpectedResponse, result);
        _mediatorMock
            .Verify(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == mediatorSetupResult.Id), default), Times.Once);
    }

    [Fact]
    public async Task GetBalance_ShouldReturnOk_WhenAccountExists()
    {
        await Event_Mock(
            mediatorSetupResult: new AccountResult("01234", 100),
            controllerExpectedResponse: typeof(OkResult));
    }

    [Fact]
    public async Task GetBalance_ShouldReturnNotFound_WhenAccountDoesNotExist()
    {
        await Event_Mock(
            mediatorSetupResult: new AccountResult(default, default),
            controllerExpectedResponse: typeof(NotFoundResult));
    }
}