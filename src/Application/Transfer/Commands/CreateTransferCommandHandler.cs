using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using MediatR;

namespace Ebanx.Services.Account.Application.Transfer.Commands;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Domain.Transaction.Transaction>
{
    private readonly IMediator _mediator;

    public CreateTransferCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    private async Task<(Domain.Account.Account, Domain.Account.Account)> GetAccountsAsync(CreateTransferCommand request)
    {
        var originAccountTask = _mediator.Send(new GetAccountQuery(request.OriginAccountId));
        var destinationAccountTask = _mediator.Send(new GetAccountQuery(request.DestinationAccountId));

        await Task.WhenAll(originAccountTask, destinationAccountTask);

        return (originAccountTask.Result, destinationAccountTask.Result);
    }

    public async Task<Domain.Transaction.Transaction> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var (originAccount, destinationAccount) = await GetAccountsAsync(request);

        if (originAccount.Id == default || destinationAccount.Id == default)
        {
            return new Domain.Transaction.Transaction(default, default, default, default);
        }
        
        return new Domain.Transaction.Transaction(default, default, default, default);
    }
}