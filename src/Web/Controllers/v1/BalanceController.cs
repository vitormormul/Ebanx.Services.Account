using System.Net;
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
    public async Task<IActionResult> Balance([FromQuery] string account_id)
    {
        if (false)
        {
            return NotFound();
        }
        return Ok();
    }
}