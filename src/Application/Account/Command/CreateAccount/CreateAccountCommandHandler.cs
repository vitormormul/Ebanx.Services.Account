using Ebanx.Services.Account.Application.Account.Common;
using MediatR;

namespace Ebanx.Services.Account.Application.Account.Command.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountResult>
{
    public Task<AccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}