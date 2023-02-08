using MediatR;

namespace Ebanx.Services.Account.Application.General.Commands.ResetData;

public class ResetDataCommandHandler : IRequestHandler<ResetDataCommand>
{
    public Task<Unit> Handle(ResetDataCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}