using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.Account.Queries;

public class GetAccountQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAccount_WhenItIsCreated()
    {
        //Arrange
        var accountReader = new Mock<IAccountReader>();
        var handler = new GetAccountQueryHandler(accountReader.Object);
        var request = new GetAccountQuery("0123");
        var account = new Ebanx.Services.Account.Domain.Account.Account(request.Id, 100);

        accountReader
            .Setup(w => w.GetAsync(It.Is<string>(x => x == account.Id)))
            .ReturnsAsync(account);
        
        //Act
        var result = await handler.Handle(request, default);
        
        //Assert
        Assert.Equal(account, result);
    }
}