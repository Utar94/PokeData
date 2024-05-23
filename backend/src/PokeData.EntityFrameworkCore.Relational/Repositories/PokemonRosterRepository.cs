using Logitar.Data;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Roster;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class PokemonRosterRepository : IPokemonRosterRepository
{
  private readonly PokemonContext _context;
  private readonly ISqlHelper _sqlHelper;

  public PokemonRosterRepository(PokemonContext context, ISqlHelper sqlHelper)
  {
    _context = context;
    _sqlHelper = sqlHelper;
  }

  public async Task<int> DeleteAllAsync(CancellationToken cancellationToken)
  {
    ICommand command = _sqlHelper.DeleteFrom(PokemonDb.PokemonRoster.Table).Build();
    return await _context.Database.ExecuteSqlRawAsync(command.Text, command.Parameters.ToArray());
  }

  public async Task DeleteAsync(ushort speciesId, CancellationToken cancellationToken)
  {
    PokemonRosterEntity? entity = await _context.PokemonRoster
      .SingleOrDefaultAsync(x => x.PokemonSpeciesId == speciesId, cancellationToken);
    if (entity != null)
    {
      _context.PokemonRoster.Remove(entity);
      await _context.SaveChangesAsync(cancellationToken);
    }
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
