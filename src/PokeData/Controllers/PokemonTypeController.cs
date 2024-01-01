using Microsoft.AspNetCore.Mvc;
using PokeData.Contracts.Search;
using PokeData.Contracts.Types;

namespace PokeData.Controllers;

[ApiController]
[Route("pokemon/types")]
public class PokemonTypeController : ControllerBase
{
  private readonly IPokemonTypeService _typeService;

  public PokemonTypeController(IPokemonTypeService typeService)
  {
    _typeService = typeService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<PokemonType>> ReadAsync(byte id, CancellationToken cancellationToken)
  {
    PokemonType? result = await _typeService.ReadAsync(id, uniqueName: null, cancellationToken);
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("unique-name:{uniqueName}")]
  public async Task<ActionResult<PokemonType>> ReadAsync(string uniqueName, CancellationToken cancellationToken)
  {
    PokemonType? result = await _typeService.ReadAsync(number: null, uniqueName, cancellationToken);
    return result == null ? NotFound() : Ok(result);
  }

  [HttpPost("search")] // TODO(fpion): Get
  public async Task<ActionResult<SearchResults<PokemonType>>> SearchAsync([FromBody] SearchPokemonTypesPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _typeService.SearchAsync(payload, cancellationToken));
  }
}
