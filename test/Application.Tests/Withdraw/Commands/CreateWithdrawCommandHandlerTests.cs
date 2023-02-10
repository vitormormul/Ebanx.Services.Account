using System.Threading.Tasks;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Ebanx.Services.Account.Application.Withdraw.Commands;
using MediatR;
using Moq;
using Xunit;

namespace Application.Tests.Withdraw.Commands;

public class CreateWithdrawCommandHandlerTests
{
    private readonly Ebanx.Services.Account.Domain.Account.Account _accountFixture = new("01234", 100);
    private readonly CreateWithdrawCommand _commandFixture = new("01234", 100);
    private readonly CreateWithdrawCommandHandler _handler;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IAccountWriter> _writerMock;

    public CreateWithdrawCommandHandlerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _writerMock = new Mock<IAccountWriter>();
        _handler = new CreateWithdrawCommandHandler(_mediatorMock.Object, _writerMock.Object);
    }

    private void MediatorSetup(CreateWithdrawCommand command,
        Ebanx.Services.Account.Domain.Account.Account? getAccountResult,
        Ebanx.Services.Account.Domain.Account.Account? creteAccountResult)
    {
        _mediatorMock
            .Setup(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == command.AccountId), default))
            .ReturnsAsync(getAccountResult);
    }

    private void MediatorVerify(CreateWithdrawCommand command, Times getAccountTimes)
    {
        _mediatorMock
            .Verify(m => m.Send(It.Is<GetAccountQuery>(q => q.Id == command.AccountId), default), getAccountTimes);
    }

    private void WriterSetup(CreateWithdrawCommand request,
        Ebanx.Services.Account.Domain.Account.Account getAccountResult)
    {
        _writerMock
            .Setup(w => w.CreateWithdrawAsync(It.Is<Ebanx.Services.Account.Domain.Account.Account>(x =>
                x.Id == getAccountResult.Id && x.Balance == getAccountResult.Balance), It.Is<int>(x =>
                x == request.Amount)))
            .ReturnsAsync(new Ebanx.Services.Account.Domain.Transaction.Withdraw(getAccountResult with
            {
                Balance = getAccountResult.Balance - request.Amount
            }));
    }

    private void WriterVerify(Times createWithdrawTimes)
    {
        _writerMock
            .Verify(
                w => w.CreateWithdrawAsync(It.IsAny<Ebanx.Services.Account.Domain.Account.Account>(), It.IsAny<int>()),
                createWithdrawTimes);
    }

    [Fact]
    public async Task Handle_ShouldNotCreateWithdraw_WhenAccountDoesNotExist()
    {
        //Arrange
        MediatorSetup(_commandFixture, default, _accountFixture);

        //Act
        var result = await _handler.Handle(_commandFixture, default);

        //Assert
        MediatorVerify(_commandFixture, Times.Once());
        WriterVerify(Times.Never());
    }

    [Fact]
    public async Task Handle_ShouldCreateWithdraw_WhenAccountExists()
    {
        //Arrange
        MediatorSetup(_commandFixture, _accountFixture, default);

        //Act
        var result = await _handler.Handle(_commandFixture, default);

        //Assert
        MediatorVerify(_commandFixture, Times.Once());
        WriterVerify(Times.Once());
    }
}