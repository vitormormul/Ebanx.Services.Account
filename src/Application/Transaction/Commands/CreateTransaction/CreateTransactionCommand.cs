using Ebanx.Services.Account.Domain.Transaction.Entities;
using MediatR;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public record CreateTransactionCommand(
    TransactionType? Type,
    int Amount,
    string? OriginAccountId,
    string? DestinationAccountId) : IRequest<Domain.Transaction.ITransaction?>;