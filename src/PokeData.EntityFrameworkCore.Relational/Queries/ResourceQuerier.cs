using Logitar.EventSourcing;
using Microsoft.EntityFrameworkCore;
using PokeData.Application.Resources;
using PokeData.Domain.Resources;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Queries;

internal class ResourceQuerier : IResourceQuerier
{
  private readonly DbSet<ResourceEntity> _resources;

  public ResourceQuerier(PokemonContext context)
  {
    _resources = context.Resources;
  }

  public async Task<Resource?> ReadBySourceAsync(string source, CancellationToken cancellationToken)
  {
    string sourceNormalized = source.Trim().ToUpper();

    ResourceEntity? entity = await _resources.AsNoTracking()
      .SingleOrDefaultAsync(x => x.SourceNormalized == sourceNormalized, cancellationToken);
    if (entity == null)
    {
      return null;
    }

    AggregateId id = new(entity.AggregateId);

    return new Resource(new ResourceId(id), new SourceUnit(entity.Source), new JsonStringUnit(entity.Json));
  }
}
