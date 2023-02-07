using Ebanx.Services.Account.Application.Account.Common;
using MediatR;

namespace Ebanx.Services.Account.Application.Account.Queries.GetAccount;

public record GetAccountQuery(string Id) : IRequest<AccountResult>;