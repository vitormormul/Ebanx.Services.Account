using Ebanx.Services.Account.Application.Transaction.Common;
using MediatR;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResult>
{
    public Task<TransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}