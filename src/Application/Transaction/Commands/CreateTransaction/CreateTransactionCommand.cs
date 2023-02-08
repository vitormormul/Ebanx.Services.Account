using Ebanx.Services.Account.Domain.Account.ValueObjects;
using Ebanx.Services.Account.Domain.Transaction.Entities;
using MediatR;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public record CreateTransactionCommand(
    TransactionType Type,
    int Amount,
    AccountId? OriginAccount,
    AccountId? DestinationAccount) : IRequest<Domain.Transaction.Transaction>;