using Ebanx.Services.Account.Application.Transaction.Common;
using MediatR;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public record CreateTransactionCommand : IRequest<TransactionResult>;