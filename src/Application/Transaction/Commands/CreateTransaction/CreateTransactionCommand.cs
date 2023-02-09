using Ebanx.Services.Account.Domain.Transaction.Entities;
using MediatR;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public record CreateTransactionCommand(
    TransactionType Type,
    int Amount,
    string? OriginAccount,
    string? DestinationAccount) : IRequest<Domain.Transaction.Transaction>;