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

    public async Task<Withdraw> CreateWithdrawAsync(Domain.Account.Account account, int amount)
    {
        account.Withdraw(amount);
        return new Withdraw(await _accountRepository.UpdateAsync(account));
    }

    public async Task<Transfer> CreateTransferAsync(Domain.Account.Account originAccount,
        Domain.Account.Account destinationAccount, int amount)
    {
        var originAccountTask = CreateWithdrawAsync(originAccount, amount);
        var destinationAccountTask = CreateDepositAsync(destinationAccount, amount);

        await Task.WhenAll(originAccountTask, destinationAccountTask);

        var withdraw = originAccountTask.Result;
        var deposit = destinationAccountTask.Result;
        return new Transfer(withdraw.Origin, deposit.Destination);
    }

    public async Task ClearTable()
    {
        await _accountRepository.ClearTableAsync();
    }
}