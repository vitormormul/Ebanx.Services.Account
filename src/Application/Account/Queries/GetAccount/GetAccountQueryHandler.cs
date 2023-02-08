using MediatR;

namespace Ebanx.Services.Account.Application.Account.Queries.GetAccount;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Domain.Account.Account>
{
    public Task<Domain.Account.Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Domain.Account.Account(request.Id, 2121));
    }
}