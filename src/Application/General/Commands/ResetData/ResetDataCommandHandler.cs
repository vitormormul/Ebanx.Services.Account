using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using MediatR;

namespace Ebanx.Services.Account.Application.General.Commands.ResetData;

public class ResetDataCommandHandler : IRequestHandler<ResetDataCommand>
{
    private readonly IAccountWriter _accountWriter;

    public ResetDataCommandHandler(IAccountWriter accountWriter)
    {
        _accountWriter = accountWriter;
    }

    public async Task<Unit> Handle(ResetDataCommand request, CancellationToken cancellationToken)
    {
        await _accountWriter.ClearTable();
        return Unit.Value;
    }
}