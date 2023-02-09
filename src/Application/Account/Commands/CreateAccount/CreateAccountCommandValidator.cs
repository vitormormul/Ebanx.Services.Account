using FluentValidation;

namespace Ebanx.Services.Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Id).Matches(@"^[0-9]+$");
        RuleFor(command => command.Balance).GreaterThanOrEqualTo(0);
    }
}