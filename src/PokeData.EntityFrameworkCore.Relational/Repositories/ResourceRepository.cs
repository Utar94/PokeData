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
    ResourceEntity[] entities = await _context.Resources.AsNoTracking()
      .ToArrayAsync(cancellationToken);

    return entities.Select(Map);
  }

  public async Task<Resource?> GetAsync(Uri source, CancellationToken cancellationToken)
  {
    string sourceNormalized = source.ToString().ToUpper();

    ResourceEntity? entity = await _context.Resources.AsNoTracking()
      .SingleOrDefaultAsync(x => x.SourceNormalized == sourceNormalized, cancellationToken);

    return entity == null ? null : Map(entity);
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

  private static Resource Map(ResourceEntity resource) => new(new Uri(resource.Source), new Content(resource.ContentType, resource.ContentText));
}
