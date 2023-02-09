using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using MediatR;

namespace Ebanx.Services.Account.Application.Withdraw.Commands;

public class CreateWithdrawCommandHandler : IRequestHandler<CreateWithdrawCommand, Domain.Transaction.Withdraw?>
{
    private readonly IAccountWriter _accountWriter;
    private readonly IMediator _mediator;

    public CreateWithdrawCommandHandler(IMediator mediator, IAccountWriter accountWriter)
    {
        _mediator = mediator;
        _accountWriter = accountWriter;
    }

    public async Task<Domain.Transaction.Withdraw?> Handle(CreateWithdrawCommand request,
        CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetAccountQuery(request.AccountId));

        if (account == default) return default;

        return await _accountWriter.CreateWithdrawAsync(account, request.Amount);
    }
}