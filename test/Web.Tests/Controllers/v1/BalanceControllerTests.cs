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
    private readonly Account _accountFixture = new Account("0123", 100);

    public BalanceControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _balanceController = new BalanceController(_mediatorMock.Object);
    }

    private async Task Balance_Mock(string accountId, Account? mediatorSetupResult, (Type StatusCode, int Body) controllerExpectedResponse)
    {
        //Arrange
        _mediatorMock
            .Setup(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == accountId), default))
            .ReturnsAsync(mediatorSetupResult);

        //Act
        var result = (await _balanceController.Balance(accountId)).Result as ObjectResult;

        //Assert
        Assert.IsType(controllerExpectedResponse.StatusCode, result);
        Assert.Equal(controllerExpectedResponse.Body, result?.Value);
        _mediatorMock
            .Verify(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == accountId), default), Times.Once);
    }

    [Fact]
    public async Task GetBalance_ShouldReturnOk_WhenAccountExists()
    {
        var controllerExpectedResponse = (typeof(OkObjectResult), _accountFixture.Balance);
        await Balance_Mock(_accountFixture.Id, _accountFixture, controllerExpectedResponse);
    }

    [Fact]
    public async Task GetBalance_ShouldReturnNotFound_WhenAccountDoesNotExist()
    {
        var controllerExpectedResponse = (typeof(NotFoundObjectResult), 0);
        await Balance_Mock(_accountFixture.Id,  default, controllerExpectedResponse);
    }
}