using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokeData.Application.Resources.Commands;

namespace PokeData.Controllers;

[ApiController]
//[Authorize] // TODO(fpion): Authorization
[Route("species")]
public class SpeciesController : ControllerBase
{
  private readonly IMediator _mediator;

  public SpeciesController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost("import/{id}")]
  public async Task<ActionResult> ImportAsync(string id, CancellationToken cancellationToken)
  {
    await _mediator.Send(new ImportSpeciesResourcesCommand(id), cancellationToken);
    return NoContent();
  }
}
