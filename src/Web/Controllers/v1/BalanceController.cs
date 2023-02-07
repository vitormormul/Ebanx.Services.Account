using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Ebanx.Services.Account.Web.Controllers.v1;

/// <inheritdoc />
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BalanceController : ControllerBase
{
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