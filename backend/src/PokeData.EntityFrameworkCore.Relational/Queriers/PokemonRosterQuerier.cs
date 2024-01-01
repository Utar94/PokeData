using Microsoft.EntityFrameworkCore;
using PokeData.Application.Roster;
using PokeData.Contracts.Roster;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Queriers;

internal class PokemonRosterQuerier : IPokemonRosterQuerier
{
  private readonly DbSet<PokemonSpeciesEntity> _species;

  public PokemonRosterQuerier(PokemonContext context)
  {
    _species = context.PokemonSpecies;
  }

  public async Task<PokemonRoster> ReadAsync(CancellationToken cancellationToken)
  {
    PokemonSpeciesEntity[] species = await _species.AsNoTracking()
      .Include(x => x.Generation).ThenInclude(x => x!.MainRegion)
      .Include(x => x.Varieties).ThenInclude(x => x.PrimaryType)
      .Include(x => x.Varieties).ThenInclude(x => x.SecondaryType)
      .Include(x => x.Roster).ThenInclude(x => x!.Region)
      .Include(x => x.Roster).ThenInclude(x => x!.PrimaryType)
      .Include(x => x.Roster).ThenInclude(x => x!.SecondaryType)
      .OrderBy(x => x.PokemonSpeciesId)
      .ToArrayAsync(cancellationToken);

    return new Mapper().ToRoster(species);
  }
}
