using Ebanx.Services.Account.Domain.Transaction.Entities;

namespace Ebanx.Services.Account.Domain.Transaction;

public record Transaction(
    TransactionType? Type,
    string? DestinationAccountId,
    string? OriginAccountId,
    int Amount);