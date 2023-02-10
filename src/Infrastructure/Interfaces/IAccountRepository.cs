namespace Ebanx.Services.Account.Infrastructure.Interfaces;

public interface IAccountRepository
{
    Task<Domain.Account.Account?> GetAsync(string accountId);
    Task<Domain.Account.Account> AddAsync(Domain.Account.Account account);
    Task<Domain.Account.Account> UpdateAsync(Domain.Account.Account account);
    Task ClearTableAsync();
}