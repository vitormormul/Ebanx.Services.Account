using Ebanx.Services.Account.Domain.Transaction.Entities;

namespace Ebanx.Services.Account.Domain.Transaction;

public record Transaction(
    TransactionType? Type,
    Account.Account? Destination,
    Account.Account? Origin,
    int Amount);