using Ebanx.Services.Account.Application.Account.Common;
using MediatR;

namespace Ebanx.Services.Account.Application.Account.Command.CreateAccount;

public record CreateAccountCommand(string Id) : IRequest<AccountResult>;