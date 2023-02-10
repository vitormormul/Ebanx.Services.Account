using Ebanx.Services.Account.Domain.Transaction.Entities;
using FluentValidation;

namespace Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(command => command.Type).IsInEnum();
        RuleFor(command => command.Type).NotEqual(TransactionType.None);
        RuleFor(command => command.Amount).GreaterThanOrEqualTo(0);
    }
}