using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using MediatR;

namespace Ebanx.Services.Account.Application.Transfer.Commands;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Domain.Transaction.Transfer?>
{
    private readonly IAccountWriter _accountWriter;
    private readonly IMediator _mediator;

    public CreateTransferCommandHandler(IMediator mediator, IAccountWriter accountWriter)
    {
        _mediator = mediator;
        _accountWriter = accountWriter;
    }

    public async Task<Domain.Transaction.Transfer?> Handle(CreateTransferCommand request,
        CancellationToken cancellationToken)
    {
        var (originAccount, destinationAccount) = await GetAccountsAsync(request);

        if (originAccount == default || destinationAccount == default)
            return default;

        return await _accountWriter.CreateTransferAsync(originAccount, destinationAccount, request.Amount);
    }

    private async Task<(Domain.Account.Account?, Domain.Account.Account?)> GetAccountsAsync(
        CreateTransferCommand request)
    {
        var originAccountTask = _mediator.Send(new GetAccountQuery(request.OriginAccountId));
        var destinationAccountTask = _mediator.Send(new GetAccountQuery(request.DestinationAccountId));

        await Task.WhenAll(originAccountTask, destinationAccountTask);

        return (originAccountTask.Result, destinationAccountTask.Result);
    }
}