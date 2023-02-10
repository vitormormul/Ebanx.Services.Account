using Ebanx.Services.Account.Application.Common.Constants;
using FluentValidation;

namespace Ebanx.Services.Account.Application.Deposit.Commands;

public class CreateDepositCommandValidator : AbstractValidator<CreateDepositCommand>
{
    public CreateDepositCommandValidator()
    {
        RuleFor(command => command.AccountId).NotEmpty();
        RuleFor(command => command.AccountId).Matches(RegexConstants.OnlyNumbers);
        RuleFor(command => command.Amount).GreaterThanOrEqualTo(0);
    }
}