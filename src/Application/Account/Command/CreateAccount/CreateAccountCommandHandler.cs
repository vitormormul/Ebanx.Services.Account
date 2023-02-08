using MediatR;

namespace Ebanx.Services.Account.Application.Account.Command.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Domain.Account.Account>
{
    public Task<Domain.Account.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}