using System.Threading.Tasks;
using Ebanx.Services.Account.Application.General.Commands.ResetData;
using Ebanx.Services.Account.Web.Controllers.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Web.Tests.Controllers.v1;

public class ResetControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ResetController _resetController;
    
    public ResetControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _resetController = new ResetController(_mediatorMock.Object);
    }

    [Fact]
    private async Task PostReset_ShouldReturnOk_WhenMediatorDoesNotThrowError()
    {
        //Act
        var result = await _resetController.Reset();

        //Assert
        Assert.IsType<OkResult>(result);
        _mediatorMock
            .Verify(m => m.Send(It.IsAny<ResetDataCommand>(), default), Times.Once);
    }
}