using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class ResourceRepository : IResourceRepository
{
  private readonly PokemonContext _context;

  public ResourceRepository(PokemonContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Resource>> GetAsync(CancellationToken cancellationToken)
  {
    ResourceEntity[] resources = await _context.Resources.AsNoTracking()
      .ToArrayAsync(cancellationToken);

    return resources.Select(DomainMapper.ToResource);
  }

  public async Task<Resource?> GetAsync(Uri source, CancellationToken cancellationToken)
  {
    string sourceNormalized = source.ToString().ToUpper();

    ResourceEntity? resource = await _context.Resources.AsNoTracking()
      .SingleOrDefaultAsync(x => x.SourceNormalized == sourceNormalized, cancellationToken);

    return resource == null ? null : DomainMapper.ToResource(resource);
  }

  public async Task SaveAsync(Resource resource, CancellationToken cancellationToken)
  {
    string sourceNormalized = resource.Source.ToString().ToUpper();
    ResourceEntity? entity = await _context.Resources
      .SingleOrDefaultAsync(x => x.SourceNormalized == sourceNormalized, cancellationToken);
    if (entity == null)
    {
      entity = new(resource);
      _context.Resources.Add(entity);
    }
    else
    {
      entity.Update(resource);
    }

    await _context.SaveChangesAsync(cancellationToken);
  }
}
