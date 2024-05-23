using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Regions;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class RegionRepository : IRegionRepository
{
  private readonly PokemonContext _context;

  public RegionRepository(PokemonContext context)
  {
    _context = context;
  }

  public async Task<RegionAggregate?> LoadAsync(string idOrUniqueName, CancellationToken cancellationToken)
  {
    RegionEntity? region = null;

    if (byte.TryParse(idOrUniqueName, out byte id))
    {
      region = await _context.Regions.AsNoTracking()
        .Include(x => x.MainGeneration)
        .SingleOrDefaultAsync(x => x.RegionId == id, cancellationToken);
    }

    if (region == null)
    {
      string uniqueNameNormalized = PokemonDb.Normalize(idOrUniqueName);

      region = await _context.Regions.AsNoTracking()
        .Include(x => x.MainGeneration)
        .SingleOrDefaultAsync(x => x.UniqueNameNormalized == uniqueNameNormalized, cancellationToken);
    }

    return region == null ? null : DomainMapper.ToRegion(region);
  }

  public async Task SaveAsync(RegionAggregate region, CancellationToken cancellationToken)
  {
    RegionEntity? entity = await _context.Regions
      .SingleOrDefaultAsync(x => x.RegionId == region.Number, cancellationToken);
    if (entity == null)
    {
      entity = new(region);
      _context.Regions.Add(entity);
    }
    else
    {
      entity.Update(region);
    }

    await _context.SaveChangesAsync(cancellationToken);
  }
}
