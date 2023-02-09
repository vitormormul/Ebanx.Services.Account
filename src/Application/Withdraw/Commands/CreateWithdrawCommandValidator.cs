using Ebanx.Services.Account.Application.Common.Constants;
using FluentValidation;

namespace Ebanx.Services.Account.Application.Withdraw.Commands;

public class CreateWithdrawCommandValidator : AbstractValidator<CreateWithdrawCommand>
{
    public CreateWithdrawCommandValidator()
    {
        RuleFor(command => command.AccountId).NotEmpty();
        RuleFor(command => command.AccountId).Matches(RegexConstants.OnlyNumbers);
        RuleFor(command => command.Amount).GreaterThanOrEqualTo(0);
    }
}