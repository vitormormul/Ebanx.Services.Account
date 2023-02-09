using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Deposit.Commands;
using Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;
using Ebanx.Services.Account.Domain.Transaction.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Application.Tests.Transaction.Commands;

public class CreateTransactionCommandHandlerTests
{
    private readonly CreateTransactionCommandHandler _handler;
    private readonly Mock<IMediator> _mediatorMock;

    public CreateTransactionCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _handler = new CreateTransactionCommandHandler(_mediatorMock.Object);
    }

    private async Task Handle_Mock<TCommandType>(CreateTransactionCommand command,
        Expression<Func<TCommandType, bool>> mediatorSendRequirements,
        Ebanx.Services.Account.Domain.Transaction.Transaction mediatorSetupResult,
        Expression<Func<TCommandType, bool>> mediatorVerifyRequirements)
    {
        //Arrange
        _mediatorMock
            .Setup(m => m.Send(It.Is(mediatorSendRequirements)!, default))
            .ReturnsAsync(mediatorSetupResult);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        _mediatorMock
            .Verify(m => m.Send(It.Is(mediatorVerifyRequirements)!, default), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendCreateDepositCommand_WhenTransactionTypeIsDeposit()
    {
        //Arrange
        var account = new Ebanx.Services.Account.Domain.Account.Account("01234", 100);
        var transaction =
            new Ebanx.Services.Account.Domain.Transaction.Transaction(TransactionType.Deposit, account, default, 100);
        var command = new CreateTransactionCommand(TransactionType.Deposit, transaction.Amount, default,
            transaction.Destination!.Id);
        
        _mediatorMock
            .Setup(m => m.Send(It.Is<CreateDepositCommand>(c => c.AccountId == transaction.Destination!.Id && c.Amount == transaction.Amount), default))
            .ReturnsAsync(transaction);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        _mediatorMock
            .Verify(m => m.Send(It.Is<CreateDepositCommand>(c => c.AccountId == transaction.Destination!.Id && c.Amount == transaction.Amount), default), Times.Once);
        
        
        
        //var command = new CreateTransactionCommand(TransactionType.Deposit, 4321, default, new AccountId("01234"));
        //var account = new Ebanx.Services.Account.Domain.Account.Account(command.DestinationAccount!.Value, command.Amount);
        //
        //await Handle_Mock<CreateDepositCommand>(
        //    command,
        //    c => c.AccountId == command.DestinationAccount!.Value && c.Amount == command.Amount,
        //    new Ebanx.Services.Account.Domain.Transaction.Transaction(TransactionType.Deposit, account, default, default),
        //    mediatorVerifyRequirements: c => c.AccountId == command.DestinationAccount!.Value && c.Amount == command.Amount);
    }
}