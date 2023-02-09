using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using MediatR;

namespace Ebanx.Services.Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Domain.Account.Account>
{
    private readonly IAccountWriter _accountWriter;

    public CreateAccountCommandHandler(IAccountWriter accountWriter)
    {
        _accountWriter = accountWriter;
    }

    public async Task<Domain.Account.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var newAccount = new Domain.Account.Account(request.Id, request.Balance);
        return await _accountWriter.CreateAsync(newAccount);
    }
}