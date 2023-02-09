using Ebanx.Services.Account.Application.Account.Commands.CreateAccount;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using MediatR;

namespace Ebanx.Services.Account.Application.Deposit.Commands;

public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, Domain.Transaction.Deposit>
{
    private readonly IAccountWriter _accountWriter;
    private readonly IMediator _mediator;

    public CreateDepositCommandHandler(IMediator mediator, IAccountWriter accountWriter)
    {
        _mediator = mediator;
        _accountWriter = accountWriter;
    }

    public async Task<Domain.Transaction.Deposit> Handle(CreateDepositCommand request,
        CancellationToken cancellationToken)
    {
        //TODO: create validators
        var account = await _mediator.Send(new GetAccountQuery(request.AccountId), cancellationToken);

        if (account == default)
        {
            var createdAccount = await _mediator.Send(new CreateAccountCommand(request.AccountId, request.Amount),
                cancellationToken);
            return new Domain.Transaction.Deposit(createdAccount);
        }

        return await _accountWriter.CreateDepositAsync(account, request.Amount);
    }
}