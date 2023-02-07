using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Ebanx.Services.Account.Controllers.v1;

/// <inheritdoc />
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class EventController : ControllerBase
{
    /// <summary>
    /// Create operations an existing account.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Balance()
    {
        if (false)
        {
            return NotFound();
        }
        return Ok();
    }
}