using System.Net;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Application.Transaction.Commands.CreateTransaction;
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
    /// Create transactions.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Event(string account_id)
    {
        var account = await _mediator.Send(new GetAccountQuery(account_id));

        if (account.Id == default)
        {
            return NotFound();
        }

        return Ok();
    }
}