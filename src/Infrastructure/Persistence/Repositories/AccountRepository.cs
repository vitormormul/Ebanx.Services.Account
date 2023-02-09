using Ebanx.Services.Account.Infrastructure.Interfaces;

namespace Ebanx.Services.Account.Infrastructure.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDbContext _accountDbContext;

    public AccountRepository(AccountDbContext accountDbContext)
    {
        _accountDbContext = accountDbContext;
    }

    public async Task<Domain.Account.Account?> GetAsync(string accountId)
    {
        return await _accountDbContext.FindAsync<Domain.Account.Account>(accountId);
    }

    public async Task<Domain.Account.Account> AddAsync(Domain.Account.Account account)
    {
        _accountDbContext.Add(account);
        await _accountDbContext.SaveChangesAsync();
        return account;
    }

    public async Task<Domain.Account.Account> UpdateAsync(Domain.Account.Account account)
    {
        _accountDbContext.Update(account);
        await _accountDbContext.SaveChangesAsync();
        return await GetAsync(account.Id);
    }
}