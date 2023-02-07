using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Ebanx.Services.Account.Web.Controllers.v1;

/// <inheritdoc />
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ResetController : ControllerBase
{
    /// <summary>
    /// Reset state before starting tests.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Reset()
    {
        return Ok();
    }
}