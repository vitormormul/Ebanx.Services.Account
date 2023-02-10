using MediatR;

namespace Ebanx.Services.Account.Application.Withdraw.Commands;

public record CreateWithdrawCommand(string AccountId, int Amount) : IRequest<Domain.Transaction.Withdraw?>;