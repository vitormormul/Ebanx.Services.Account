using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Ebanx.Services.Account.Application.General.Commands.ResetData;
using Moq;
using Xunit;

namespace Application.Tests.General.Command;

public class ResetDataCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldClearTable_WhenCalled()
    {
        //Arrange
        var writerMock = new Mock<IAccountWriter>();
        var handler = new ResetDataCommandHandler(writerMock.Object);

        //Act
        await handler.Handle(new ResetDataCommand(), default);

        //Assert
        writerMock.Verify(w => w.ClearTable(), Times.Once);
    }
}