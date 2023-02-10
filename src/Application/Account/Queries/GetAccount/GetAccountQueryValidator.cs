using Ebanx.Services.Account.Application.Common.Constants;
using FluentValidation;

namespace Ebanx.Services.Account.Application.Account.Queries.GetAccount;

public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
{
    public GetAccountQueryValidator()
    {
        RuleFor(query => query.Id).NotEmpty();
        RuleFor(query => query.Id).Matches(RegexConstants.OnlyNumbers);
    }
}