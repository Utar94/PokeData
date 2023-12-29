using Logitar.EventSourcing;
using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.EventSourcing.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class ResourceRepository : Logitar.EventSourcing.EntityFrameworkCore.Relational.AggregateRepository, IResourceRepository
{
  private readonly DbSet<ResourceEntity> _resources;

  public ResourceRepository(IEventBus eventBus, EventContext eventContext, IEventSerializer eventSerializer, PokemonContext pokemonContext)
    : base(eventBus, eventContext, eventSerializer)
  {
    _resources = pokemonContext.Resources;
  }

  public async Task<ResourceAggregate?> LoadAsync<T>(string identifier, CancellationToken cancellationToken)
    => await LoadAsync(ResourceId.Create<T>(identifier), cancellationToken);
  public async Task<ResourceAggregate?> LoadAsync(ResourceId id, CancellationToken cancellationToken)
    => await LoadAsync<ResourceAggregate>(id.AggregateId, cancellationToken);
  public async Task<ResourceAggregate?> LoadAsync(SourceUnit source, CancellationToken cancellationToken)
  {
    string sourceNormalized = source.Value.Trim().ToUpper();

    ResourceEntity? entity = await _resources.AsNoTracking()
      .SingleOrDefaultAsync(x => x.SourceNormalized == sourceNormalized, cancellationToken); // TODO(fpion): optimize query using raw SQL
    if (entity == null)
    {
      return null;
    }

    AggregateId id = new(entity.AggregateId);

    return await LoadAsync<ResourceAggregate>(id, cancellationToken);
  }

  public async Task<IEnumerable<ResourceAggregate>> LoadAsync(IEnumerable<ResourceId> ids, CancellationToken cancellationToken)
  {
    AggregateId[] aggregateIds = ids.Select(id => id.AggregateId).Distinct().ToArray();
    return await LoadAsync<ResourceAggregate>(aggregateIds, cancellationToken);
  }

  public async Task SaveAsync(ResourceAggregate resource, CancellationToken cancellationToken)
    => await base.SaveAsync(resource, cancellationToken);
  public async Task SaveAsync(IEnumerable<ResourceAggregate> resources, CancellationToken cancellationToken)
    => await base.SaveAsync(resources, cancellationToken);
}
