using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Ebanx.Services.Account.Application.Transfer.Commands;
using MediatR;
using Moq;
using Xunit;

namespace Application.Tests.Transfer.Commands;

public class CreateTransferCommandHandlerTests
{
    private readonly Mock<IAccountWriter> _accountWriter;

    private readonly CreateTransferCommandHandler _handler;
    private readonly Mock<IMediator> _mediatorMock;

    public CreateTransferCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _accountWriter = new Mock<IAccountWriter>();
        _accountWriter
            .Setup(w => w.CreateTransferAsync(
                It.IsAny<Ebanx.Services.Account.Domain.Account.Account>(),
                It.IsAny<Ebanx.Services.Account.Domain.Account.Account>(), default))
            .ReturnsAsync(TransferFixture);

        _handler = new CreateTransferCommandHandler(_mediatorMock.Object, _accountWriter.Object);
    }

    private Ebanx.Services.Account.Domain.Account.Account AccountFixture => new("1234", 100);

    private Ebanx.Services.Account.Domain.Transaction.Transfer TransferFixture =>
        new(AccountFixture, AccountFixture);

    private async Task HandleTest(Ebanx.Services.Account.Domain.Account.Account? originAccountResult,
        Ebanx.Services.Account.Domain.Account.Account? destinationAccountResult, Times writerTimes)
    {
        //Arrange
        _mediatorMock
            .SetupSequence(m => m.Send(It.IsAny<GetAccountQuery>(), default))
            .ReturnsAsync(originAccountResult)
            .ReturnsAsync(destinationAccountResult);
        ;

        //Act
        await _handler.Handle(new CreateTransferCommand("1234", "4321", 100), default);

        //Assert
        _mediatorMock
            .Verify(m => m.Send(It.IsAny<GetAccountQuery>(), default), Times.Exactly(2));
        _accountWriter
            .Verify(w => w.CreateTransferAsync(
                It.IsAny<Ebanx.Services.Account.Domain.Account.Account>(),
                It.IsAny<Ebanx.Services.Account.Domain.Account.Account>(),
                It.IsAny<int>()), writerTimes);
    }

    [Fact]
    public async Task Handle_ShouldCreateTransfer_WhenAccountsExist()
    {
        await HandleTest(AccountFixture, AccountFixture, Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldNotCreateTransfer_WhenOriginAccountsDoNotExist()
    {
        await HandleTest(default, AccountFixture, Times.Never());
    }

    [Fact]
    public async Task Handle_ShouldCreateAccountAndCreateTransfer_WhenDestinationAccountsDoesNotExist()
    {
        await HandleTest(AccountFixture, default, Times.Once());
    }
}