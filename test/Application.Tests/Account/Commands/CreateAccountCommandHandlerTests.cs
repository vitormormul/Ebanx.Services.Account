using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Commands.CreateAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.Account.Commands;

public class CreateAccountCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAccount_WhenItIsCreated()
    {
        //Arrange
        var accountWriter = new Mock<IAccountWriter>();
        var handler = new CreateAccountCommandHandler(accountWriter.Object);
        var request = new CreateAccountCommand("0123", 100);
        var account = new Ebanx.Services.Account.Domain.Account.Account(request.Id, request.Balance);

        accountWriter
            .Setup(w => w.CreateAsync(It.Is<Ebanx.Services.Account.Domain.Account.Account>(
                x => x.Id == request.Id && x.Balance == request.Balance)))
            .ReturnsAsync(account);

        //Act
        var result = await handler.Handle(request, default);

        //Assert
        Assert.Equal(account, result);
    }
}