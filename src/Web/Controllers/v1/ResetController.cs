using System.Net;
using Ebanx.Services.Account.Application.General.Commands.ResetData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS1591

namespace Ebanx.Services.Account.Web.Controllers.v1;

[ApiController]
[Route("[controller]")]
[Produces("text/plain")]
public class ResetController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Reset state before starting tests.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> Reset()
    {
        await _mediator.Send(new ResetDataCommand());
        return Ok("OK");
    }
}