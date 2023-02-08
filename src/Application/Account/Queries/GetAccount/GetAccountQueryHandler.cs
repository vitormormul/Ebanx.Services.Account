using Ebanx.Services.Account.Application.Account.Common;
using MediatR;

namespace Ebanx.Services.Account.Application.Account.Queries.GetAccount;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountResult>
{
    public Task<AccountResult> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}