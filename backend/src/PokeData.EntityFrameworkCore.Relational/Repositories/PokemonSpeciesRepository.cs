using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Species;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class PokemonSpeciesRepository : IPokemonSpeciesRepository
{
  private readonly PokemonContext _context;

  public PokemonSpeciesRepository(PokemonContext context)
  {
    _context = context;
  }

  public async Task<PokemonSpecies?> LoadAsync(ushort id, CancellationToken cancellationToken)
  {
    PokemonSpeciesEntity? species = await _context.PokemonSpecies.AsNoTracking()
      .Include(x => x.Generation).ThenInclude(x => x!.MainRegion)
      .Include(x => x.Varieties).ThenInclude(x => x.PrimaryType)
      .Include(x => x.Varieties).ThenInclude(x => x.SecondaryType)
      .SingleOrDefaultAsync(x => x.PokemonSpeciesId == id, cancellationToken);

    return species == null ? null : DomainMapper.ToPokemonSpecies(species);
  }
}
