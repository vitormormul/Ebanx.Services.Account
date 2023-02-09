using Ebanx.Services.Account.Application.Deposit.Commands;
using Ebanx.Services.Account.Application.Transfer.Commands;
using Ebanx.Services.Account.Application.Withdraw.Commands;
using Ebanx.Services.Account.Domain.Transaction;
using Ebanx.Services.Account.Domain.Transaction.Entities;
using MediatR;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ITransaction?>
{
    private readonly IMediator _mediator;

    public CreateTransactionCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ITransaction?> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        switch (request.Type)
        {
            //TODO: create validators
            case TransactionType.Deposit:
                return await _mediator.Send(new CreateDepositCommand(request.DestinationAccountId!, request.Amount),
                    cancellationToken);
            case TransactionType.Transfer:
                return await _mediator.Send(
                    new CreateTransferCommand(request.OriginAccountId, request.DestinationAccountId, request.Amount),
                    cancellationToken);
            case TransactionType.Withdraw:
                return await _mediator.Send(new CreateWithdrawCommand(request.OriginAccountId, request.Amount),
                    cancellationToken);
            default:
                return default;
        }
    }
}