using Ebanx.Services.Account.Application.Common.Constants;
using FluentValidation;

namespace Ebanx.Services.Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Id).Matches(RegexConstants.OnlyNumbers);
        RuleFor(command => command.Balance).GreaterThanOrEqualTo(0);
    }
}