using MediatR;

namespace Ebanx.Services.Account.Application.Account.Queries.GetAccount;

public record GetAccountQuery(string Id) : IRequest<Domain.Account.Account>;