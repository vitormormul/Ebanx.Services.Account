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

    public async Task<Transaction> CreateDepositAsync(Transaction transaction)
    {
        var account = transaction.Destination! with { Balance = transaction.Destination.Balance + transaction.Amount };

        return transaction with { Origin = await _accountRepository.UpdateAsync(account) };
    }

    public async Task<Transaction> CreateWithdrawAsync(Transaction transaction)
    {
        var account = transaction.Origin! with { Balance = transaction.Origin.Balance - transaction.Amount };

        return transaction with { Destination = await _accountRepository.UpdateAsync(account) };
    }

    public async Task<Transaction> CreateTransferAsync(Transaction transaction)
    {
        var originAccountTask = CreateWithdrawAsync(transaction);
        var destinationAccountTask = CreateDepositAsync(transaction);

        await Task.WhenAll(originAccountTask, destinationAccountTask);

        var originAccount = originAccountTask.Result;
        var destinationAccount = destinationAccountTask.Result;

        return new Transaction(default, destinationAccount.Destination, originAccount.Origin, default);
    }

    public async Task ClearTable()
    {
        await _accountRepository.ClearTableAsync();
    }
}