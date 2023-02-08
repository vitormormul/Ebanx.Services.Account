using MediatR;

namespace Ebanx.Services.Account.Application.Account.Command.CreateAccount;

public record CreateAccountCommand(string Id, int Balance) : IRequest<Domain.Account.Account>;