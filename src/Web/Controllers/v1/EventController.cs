using System.Net;
using Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;
using Ebanx.Services.Account.Web.Contracts.Transaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS1591

namespace Ebanx.Services.Account.Web.Controllers.v1;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Create transactions.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Event([FromBody] CreateTransactionRequest request)
    {
        var command = new CreateTransactionCommand(request.Type, request.Amount, request.OriginAccount,
            request.DestinationAccount);

        var result = await _mediator.Send(command);

        if (result == default) return NotFound();

        return Ok();
    }
}