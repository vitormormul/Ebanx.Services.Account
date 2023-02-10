using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Commands.CreateAccount;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Ebanx.Services.Account.Application.Deposit.Commands;
using MediatR;
using Moq;
using Xunit;

namespace Application.Tests.Deposit.Commands;

public class CreateDepositCommandHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IAccountWriter> _writerMock;
    private readonly CreateDepositCommandHandler _handler;
    private readonly CreateDepositCommand _commandFixture = new("01234", 100);
    private readonly Ebanx.Services.Account.Domain.Account.Account _accountFixture = new("01234", 100);

    public CreateDepositCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>() ;
        _writerMock = new Mock<IAccountWriter>();
        _handler = new CreateDepositCommandHandler(_mediatorMock.Object, _writerMock.Object);
    }

    private void MediatorSetup(CreateDepositCommand command, Ebanx.Services.Account.Domain.Account.Account? getAccountResult, Ebanx.Services.Account.Domain.Account.Account? creteAccountResult)
    {
        _mediatorMock
            .Setup(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == command.AccountId), default))
            .ReturnsAsync(getAccountResult);

        _mediatorMock
            .Setup(m => m.Send(It.Is<CreateAccountCommand>(c =>
                c.Id == command.AccountId && c.Balance == command.Amount), default))!
            .ReturnsAsync(creteAccountResult);
    }

    private void MediatorVerify(CreateDepositCommand command, Times getAccountTimes, Times createAccountTimes)
    {
        _mediatorMock
            .Verify(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == command.AccountId), default), getAccountTimes);
        _mediatorMock
            .Verify(m => m.Send(It.Is<CreateAccountCommand>(c =>
                c.Id == command.AccountId && c.Balance == command.Amount), default), createAccountTimes);
    }

    private void WriterSetup(CreateDepositCommand request, Ebanx.Services.Account.Domain.Account.Account getAccountResult)
    {
        _writerMock
            .Setup(w => w.CreateDepositAsync(It.Is<Ebanx.Services.Account.Domain.Account.Account>(x =>
                x.Id == getAccountResult.Id && x.Balance == getAccountResult.Balance), It.Is<int>(x =>
                x == request.Amount)))
            .ReturnsAsync(new Ebanx.Services.Account.Domain.Transaction.Deposit(getAccountResult with
            {
                Balance = getAccountResult.Balance + request.Amount
            }));
    }

    private void WriterVerify(Times createDepositTimes)
    {
        _writerMock
            .Verify(w => w.CreateDepositAsync(It.IsAny<Ebanx.Services.Account.Domain.Account.Account>(), It.IsAny<int>()), createDepositTimes);
    }

    [Fact]
    public async Task Handle_ShouldCreateAccount_WhenItDoesNotExist()
    {
        //Arrange
        MediatorSetup(_commandFixture, default, _accountFixture);
        
        //Act
        var result = await _handler.Handle(_commandFixture, default);
        
        //Assert
        MediatorVerify(_commandFixture, Times.Once(), Times.Once());
        WriterVerify(Times.Never());
    }

    [Fact]
    public async Task Handle_ShouldCreateDeposit_WhenAccountExists()
    {
        //Arrange
        MediatorSetup(_commandFixture, _accountFixture, default);
        
        //Act
        var result = await _handler.Handle(_commandFixture, default);
        
        //Assert
        MediatorVerify(_commandFixture, Times.Once(), Times.Never());
        WriterVerify(Times.Once());
    }
}