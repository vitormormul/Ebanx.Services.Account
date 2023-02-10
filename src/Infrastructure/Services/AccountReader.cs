using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Ebanx.Services.Account.Infrastructure.Interfaces;

namespace Ebanx.Services.Account.Infrastructure.Services;

public class AccountReader : IAccountReader
{
    private readonly IAccountRepository _accountRepository;

    public AccountReader(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Domain.Account.Account?> GetAsync(string accountId)
    {
        return await _accountRepository.GetAsync(accountId);
    }
}