using Microsoft.AspNetCore.Mvc;
using PokeData.Contracts.Roster;

namespace PokeData.Controllers;

[ApiController]
[Route("pokemon/roster")]
public class PokemonRosterController : ControllerBase
{
  private readonly IPokemonRosterService _rosterService;

  public PokemonRosterController(IPokemonRosterService rosterService)
  {
    _rosterService = rosterService;
  }

  [HttpGet]
  public async Task<ActionResult<PokemonRoster>> GetAsync(CancellationToken cancellationToken)
  {
    return Ok(await _rosterService.GetAsync(cancellationToken));
  }

  [HttpPut("{speciesId}")]
  public async Task<ActionResult<SavedRosterItem>> SaveAsync(ushort speciesId, [FromBody] SaveRosterItemPayload payload, CancellationToken cancellationToken)
  {
    await _rosterService.SaveItemAsync(speciesId, payload, cancellationToken);

    PokemonRoster roster = await _rosterService.GetAsync(cancellationToken);
    SavedRosterItem result = new()
    {
      Item = roster.Items.Single(item => item.SpeciesId == speciesId),
      Stats = roster.Stats
    };
    return Ok(result);
  }
}
