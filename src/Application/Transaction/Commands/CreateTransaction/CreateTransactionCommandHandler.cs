using Ebanx.Services.Account.Domain.Transaction.Entities;
using MediatR;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Domain.Transaction.Transaction>
{
    private readonly IMediator _mediator;

    public CreateTransactionCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Domain.Transaction.Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        switch (request.Type)
        {
            case TransactionType.Deposit:
                return await _mediator.Send(request, cancellationToken);
                break;
            case TransactionType.Transfer:
                return await _mediator.Send(request, cancellationToken);
                break;
            case TransactionType.Withdraw:
                return await _mediator.Send(request, cancellationToken);
                break;
            default:
                return default;
        }
    }
}