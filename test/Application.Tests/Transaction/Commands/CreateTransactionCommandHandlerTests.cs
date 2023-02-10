using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Deposit.Commands;
using Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;
using Ebanx.Services.Account.Application.Transfer.Commands;
using Ebanx.Services.Account.Application.Withdraw.Commands;
using Ebanx.Services.Account.Domain.Transaction;
using Ebanx.Services.Account.Domain.Transaction.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Application.Tests.Transaction.Commands;

public class CreateTransactionCommandHandlerTests
{
    private readonly CreateTransactionCommand _commandFixture =
        new(TransactionType.None, 100, "3210", "01234");

    private readonly CreateTransactionCommandHandler _handler;
    private readonly Mock<IMediator> _mediatorMock;

    public CreateTransactionCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new CreateTransactionCommandHandler(_mediatorMock.Object);
    }

    private async Task HandleTest<TCommand>(CreateTransactionCommand command) where TCommand : IRequest<ITransaction?>
    {
        //Act
        await _handler.Handle(command, default);

        //Assert
        var type = typeof(TCommand);
        _mediatorMock.Verify(m => m.Send(It.IsAny<TCommand>()!, default), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendNewCommand_WhenTransactionHasType()
    {
        var testDepositTask = HandleTest<CreateDepositCommand>(_commandFixture with { Type = TransactionType.Deposit });
        var testWithdrawTask =
            HandleTest<CreateWithdrawCommand>(_commandFixture with { Type = TransactionType.Withdraw });
        var testTransferTask =
            HandleTest<CreateTransferCommand>(_commandFixture with { Type = TransactionType.Transfer });

        await Task.WhenAll(testDepositTask, testWithdrawTask, testTransferTask);
    }
}