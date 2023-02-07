using MediatR;

namespace Ebanx.Services.Account.Application.General.Command.ResetData;

public record ResetDataCommand : IRequest<Unit>;