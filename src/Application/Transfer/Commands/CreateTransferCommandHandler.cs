using Ebanx.Services.Account.Application.Account.Commands.CreateAccount;
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
        var originAccountTask = _mediator.Send(new GetAccountQuery(request.OriginAccountId));
        var destinationAccountTask = _mediator.Send(new GetAccountQuery(request.DestinationAccountId));
        
        var originAccount = await originAccountTask;
        if (originAccount == default) return default;

        var destinationAccount = await destinationAccountTask
                                 ?? await _mediator.Send(new CreateAccountCommand(request.DestinationAccountId, 0), cancellationToken);

        return await _accountWriter.CreateTransferAsync(originAccount, destinationAccount, request.Amount);
    }
}