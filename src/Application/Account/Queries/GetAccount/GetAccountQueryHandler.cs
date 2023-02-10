using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using MediatR;

namespace Ebanx.Services.Account.Application.Account.Queries.GetAccount;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Domain.Account.Account?>
{
    private readonly IAccountReader _accountReader;

    public GetAccountQueryHandler(IAccountReader accountReader)
    {
        _accountReader = accountReader;
    }

    public Task<Domain.Account.Account?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        return _accountReader.GetAsync(request.Id);
    }
}