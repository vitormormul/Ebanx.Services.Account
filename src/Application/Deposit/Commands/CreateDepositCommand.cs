using MediatR;

namespace Ebanx.Services.Account.Application.Deposit.Commands;

public record CreateDepositCommand(string AccountId, int Amount) : IRequest<Domain.Transaction.Deposit>;