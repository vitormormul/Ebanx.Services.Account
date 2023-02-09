using Ebanx.Services.Account.Application.Account.Commands.CreateAccount;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using MediatR;

namespace Ebanx.Services.Account.Application.Deposit.Commands;

public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, Domain.Transaction.Transaction>
{
    private readonly IMediator _mediator;

    public CreateDepositCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Domain.Transaction.Transaction> Handle(CreateDepositCommand request,
        CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetAccountQuery(request.AccountId), cancellationToken);

        if (account.Id == default)
        {
            var createdAccount = await _mediator.Send(new CreateAccountCommand(request.AccountId, request.Amount),
                cancellationToken);
            return new Domain.Transaction.Transaction(default, default, createdAccount, default);
        }

        return new Domain.Transaction.Transaction(default, default, default, default);
    }
}