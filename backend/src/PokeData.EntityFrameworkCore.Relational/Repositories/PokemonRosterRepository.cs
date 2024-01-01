using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Roster;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class PokemonRosterRepository : IPokemonRosterRepository
{
  private readonly PokemonContext _context;

  public PokemonRosterRepository(PokemonContext context)
  {
    _context = context;
  }

  public async Task SaveAsync(PokemonRoster roster, CancellationToken cancellationToken)
  {
    PokemonRosterEntity? entity = await _context.PokemonRoster
      .SingleOrDefaultAsync(x => x.PokemonSpeciesId == roster.Species.Number, cancellationToken);
    if (entity == null)
    {
      entity = new(roster);
      _context.PokemonRoster.Add(entity);
    }
    else
    {
      entity.Update(roster);
    }

    await _context.SaveChangesAsync(cancellationToken);
  }
}
