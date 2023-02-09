namespace Ebanx.Services.Account.Application.Common.Interfaces.Services;

public interface IAccountWriter
{
    Task<Domain.Account.Account> CreateAsync(Domain.Account.Account account);
    Task<Domain.Transaction.Deposit> CreateDepositAsync(Domain.Account.Account account, int amount);
    Task<Domain.Transaction.Transaction> CreateWithdrawAsync(Domain.Account.Account account, int amount);
    Task<Domain.Transaction.Transaction> CreateTransferAsync(Domain.Transaction.Transaction transaction);
    Task ClearTable();
}