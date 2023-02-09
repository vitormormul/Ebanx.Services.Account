namespace Ebanx.Services.Account.Application.Common.Interfaces.Services;

public interface IAccountWriter
{
    Task<Domain.Account.Account> CreateAsync(Domain.Account.Account account);
    Task<Domain.Transaction.Transaction> CreateDepositAsync(Domain.Transaction.Transaction transaction);
    Task<Domain.Transaction.Transaction> CreateWithdrawAsync(Domain.Transaction.Transaction transaction);
    Task<Domain.Transaction.Transaction> CreateTransferAsync(Domain.Transaction.Transaction transaction);
}