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

  [HttpDelete("{speciesId}")]
  public async Task<ActionResult<SavedRosterItem>> RemoveAsync(ushort speciesId, CancellationToken cancellationToken)
  {
    await _rosterService.RemoveItemAsync(speciesId, cancellationToken);
    return Ok(await GetSavedRosterItemAsync(speciesId, cancellationToken));
  }

  [HttpPut("{speciesId}")]
  public async Task<ActionResult<SavedRosterItem>> SaveAsync(ushort speciesId, [FromBody] SaveRosterItemPayload payload, CancellationToken cancellationToken)
  {
    await _rosterService.SaveItemAsync(speciesId, payload, cancellationToken);
    return Ok(await GetSavedRosterItemAsync(speciesId, cancellationToken));
  }

  private async Task<SavedRosterItem> GetSavedRosterItemAsync(ushort speciesId, CancellationToken cancellationToken)
  {
    PokemonRoster roster = await _rosterService.GetAsync(cancellationToken);
    return new SavedRosterItem
    {
      Item = roster.Items.Single(item => item.SpeciesId == speciesId),
      Stats = roster.Stats
    };
  }
}
