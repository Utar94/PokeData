using Microsoft.AspNetCore.Mvc;
using PokeData.Contracts.Regions;
using PokeData.Contracts.Search;

namespace PokeData.Controllers;

[ApiController]
[Route("regions")]
public class RegionController : ControllerBase
{
  private readonly IRegionService _regionService;

  public RegionController(IRegionService regionService)
  {
    _regionService = regionService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Region>> ReadAsync(byte id, CancellationToken cancellationToken)
  {
    Region? result = await _regionService.ReadAsync(id, uniqueName: null, cancellationToken);
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("unique-name:{uniqueName}")]
  public async Task<ActionResult<Region>> ReadAsync(string uniqueName, CancellationToken cancellationToken)
  {
    Region? result = await _regionService.ReadAsync(number: null, uniqueName, cancellationToken);
    return result == null ? NotFound() : Ok(result);
  }

  [HttpPut("{number}")]
  public async Task<ActionResult<Region>> SaveAsync(byte number, [FromBody] SaveRegionPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _regionService.SaveAsync(number, payload, cancellationToken));
  }

  [HttpPost("search")] // TODO(fpion): Get
  public async Task<ActionResult<SearchResults<Region>>> SearchAsync([FromBody] SearchRegionsPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _regionService.SearchAsync(payload, cancellationToken));
  }
}
