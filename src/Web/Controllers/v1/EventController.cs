using System.Net;
using Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;
using Ebanx.Services.Account.Domain.Transaction;
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
    /// <returns>Returns transaction which can be either a deposit, a withdraw or transfer between accounts.</returns>
    /// <param name="request">Transaction to be executed. Types are Deposit (1), Withdraw (2) and Transfer (3).</param>
    [HttpPost]
    [ProducesResponseType(typeof(ITransaction), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ITransaction>> Event([FromBody] CreateTransactionRequest request)
    {
        var command = new CreateTransactionCommand(
            Type: request.Type,
            Amount: request.Amount,
            OriginAccountId: request.OriginAccountId,
            DestinationAccountId: request.DestinationAccountId);

        var result = await _mediator.Send(command);

        if (result == default) return NotFound(0);

        return Created(new Uri("/"), result);
    }
}