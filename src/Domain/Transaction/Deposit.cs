namespace Ebanx.Services.Account.Domain.Transaction;

public record Deposit(Account.Account Destination) : ITransaction;