namespace Ebanx.Services.Account.Application.Common.Interfaces.Services;

public interface IAccountWriter
{
    Task<Domain.Account.Account> CreateAsync(Domain.Account.Account account);
    Task<Domain.Transaction.Deposit> CreateDepositAsync(Domain.Account.Account account, int amount);
    Task<Domain.Transaction.Withdraw> CreateWithdrawAsync(Domain.Account.Account account, int amount);
    Task<Domain.Transaction.Transfer> CreateTransferAsync(Domain.Account.Account originAccount, Domain.Account.Account destinationAccount, int amount);
    Task ClearTable();
}