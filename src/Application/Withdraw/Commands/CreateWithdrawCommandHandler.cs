using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using MediatR;

namespace Ebanx.Services.Account.Application.Withdraw.Commands;

public class CreateWithdrawCommandHandler : IRequestHandler<CreateWithdrawCommand, Domain.Transaction.Transaction>
{
    private readonly IMediator _mediator;

    public CreateWithdrawCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Domain.Transaction.Transaction> Handle(CreateWithdrawCommand request,
        CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetAccountQuery(request.AccountId));

        if (account.Id == default) return default;

        return default;
    }
}