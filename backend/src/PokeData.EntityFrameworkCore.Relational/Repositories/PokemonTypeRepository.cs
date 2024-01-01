using Microsoft.EntityFrameworkCore;
using PokeData.Domain;
using PokeData.Domain.Types;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class PokemonTypeRepository : IPokemonTypeRepository
{
  private readonly PokemonContext _context;

  public PokemonTypeRepository(PokemonContext context)
  {
    _context = context;
  }

  public async Task<PokemonType?> LoadAsync(byte id, CancellationToken cancellationToken)
  {
    PokemonTypeEntity? type = await _context.PokemonTypes.AsNoTracking()
      .SingleOrDefaultAsync(x => x.PokemonTypeId == id, cancellationToken);

    return type == null ? null : DomainMapper.ToPokemonType(type);
  }
}
