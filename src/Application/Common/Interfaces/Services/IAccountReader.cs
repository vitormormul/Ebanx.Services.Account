namespace Ebanx.Services.Account.Application.Common.Interfaces.Services;

public interface IAccountReader
{
    Task<Domain.Account.Account?> GetAsync(string accountId);
}