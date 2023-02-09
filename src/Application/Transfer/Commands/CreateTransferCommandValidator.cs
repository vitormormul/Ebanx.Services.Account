using Ebanx.Services.Account.Application.Common.Constants;
using FluentValidation;

namespace Ebanx.Services.Account.Application.Transfer.Commands;

public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferCommandValidator()
    {
        RuleFor(command => command.Amount).GreaterThanOrEqualTo(0);
        RuleFor(command => command.DestinationAccountId).NotEmpty();
        RuleFor(command => command.DestinationAccountId).Matches(RegexConstants.OnlyNumbers);
        RuleFor(command => command.OriginAccountId).NotEmpty();
        RuleFor(command => command.OriginAccountId).Matches(RegexConstants.OnlyNumbers);
    }
}