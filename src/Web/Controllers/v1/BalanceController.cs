using System.Net;
using Ebanx.Services.Account.Application.Account.Queries.GetAccount;
using Ebanx.Services.Account.Web.Contracts.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable CS1591

namespace Ebanx.Services.Account.Web.Controllers.v1;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BalanceController : ControllerBase
{
    private readonly IMediator _mediator;
    public BalanceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Get balance for an existing account.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<int>> Balance([FromQuery] string account_id)
    {
        var account = await _mediator.Send(new GetAccountQuery(account_id));
        
        if (account.Id == default)
        {
            return NotFound(0);
        }
        
        return Ok(account.Balance);
    }
}