using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Ebanx.Services.Account.Domain.Transaction;
using Ebanx.Services.Account.Infrastructure.Interfaces;

namespace Ebanx.Services.Account.Infrastructure.Services;

public class AccountWriter : IAccountWriter
{
    private readonly IAccountRepository _accountRepository;

    public AccountWriter(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Domain.Account.Account> CreateAsync(Domain.Account.Account account)
    {
        return await _accountRepository.AddAsync(account);
    }

    public async Task<Deposit> CreateDepositAsync(Domain.Account.Account account, int amount)
    {
        account.Deposit(amount);
        return new Deposit(await _accountRepository.UpdateAsync(account));
    }

    public async Task<Transaction> CreateWithdrawAsync(Domain.Account.Account account, int amount)
    {
        account.Withdraw(amount);

        return default;
    }

    public async Task<Transaction> CreateTransferAsync(Transaction transaction)
    {
        return default;
        //var originAccountTask = CreateWithdrawAsync(transaction);
        //var destinationAccountTask = CreateDepositAsync(transaction);

        //await Task.WhenAll(originAccountTask, destinationAccountTask);

        //var originAccount = originAccountTask.Result;
        //var destinationAccount = destinationAccountTask.Result;
        //return new Transaction(default, destinationAccount.DestinationAccount, originAccount.OriginAccountId, default);
    }

    public async Task ClearTable()
    {
        await _accountRepository.ClearTableAsync();
    }
}