namespace Ebanx.Services.Account.Domain.Transaction;

public record Transfer(Account.Account Origin, Account.Account Destination) : ITransaction;