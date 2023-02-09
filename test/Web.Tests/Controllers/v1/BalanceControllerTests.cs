using System;
using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Domain.Account;
using Ebanx.Services.Account.Web.Controllers.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Web.Tests.Controllers.v1;

public class BalanceControllerTests
{
    private readonly BalanceController _balanceController;
    private readonly Mock<IMediator> _mediatorMock;

    public BalanceControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _balanceController = new BalanceController(_mediatorMock.Object);
    }

    private async Task Balance_Mock(Account mediatorSetupResult, Type controllerExpectedResponse)
    {
        //Arrange
        _mediatorMock
            .Setup(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == mediatorSetupResult.Id), default))
            .ReturnsAsync(mediatorSetupResult);

        //Act
        var result = await _balanceController.Balance(mediatorSetupResult.Id!);

        //Assert
        Assert.IsType(controllerExpectedResponse, result.Result);
        _mediatorMock
            .Verify(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == mediatorSetupResult.Id), default), Times.Once);
    }

    [Fact]
    public async Task GetBalance_ShouldReturnOk_WhenAccountExists()
    {
        await Balance_Mock(
            new Account("01234", 100),
            typeof(OkObjectResult));
    }

    [Fact]
    public async Task GetBalance_ShouldReturnNotFound_WhenAccountDoesNotExist()
    {
        await Balance_Mock(
            new Account(default, default),
            typeof(NotFoundObjectResult));
    }
}