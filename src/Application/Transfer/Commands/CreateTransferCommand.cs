using MediatR;

namespace Ebanx.Services.Account.Application.Transfer.Commands;

public record CreateTransferCommand(string OriginAccountId, string DestinationAccountId, int Amount) : IRequest<Domain.Transaction.Transaction>;