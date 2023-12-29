using Logitar;
using Logitar.Data;
using Logitar.EventSourcing;
using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.EventSourcing.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Repositories;

internal class ResourceRepository : Logitar.EventSourcing.EntityFrameworkCore.Relational.AggregateRepository, IResourceRepository
{
  private static readonly string AggregateType = typeof(ResourceAggregate).GetNamespaceQualifiedName();

  private readonly DbSet<ResourceEntity> _resources;
  private readonly ISqlHelper _sqlHelper;

  public ResourceRepository(IEventBus eventBus, EventContext eventContext, IEventSerializer eventSerializer, PokemonContext pokemonContext, ISqlHelper sqlHelper)
    : base(eventBus, eventContext, eventSerializer)
  {
    _resources = pokemonContext.Resources;
    _sqlHelper = sqlHelper;
  }

  public async Task<ResourceAggregate?> LoadAsync<T>(string identifier, CancellationToken cancellationToken)
    => await LoadAsync(ResourceId.Create<T>(identifier), cancellationToken);
  public async Task<ResourceAggregate?> LoadAsync(ResourceId id, CancellationToken cancellationToken)
    => await LoadAsync<ResourceAggregate>(id.AggregateId, cancellationToken);
  public async Task<ResourceAggregate?> LoadAsync(SourceUnit source, CancellationToken cancellationToken)
  {
    string sourceNormalized = source.Value.Trim().ToUpper();

    IQuery query = _sqlHelper.QueryFrom(Db.Events.Table)
      .Join(Db.Resources.AggregateId, Db.Events.AggregateId,
        new OperatorCondition(Db.Events.AggregateType, Operators.IsEqualTo(AggregateType))
      )
      .Where(Db.Resources.SourceNormalized, Operators.IsEqualTo(sourceNormalized))
      .SelectAll(Db.Events.Table)
      .Build();

    EventEntity[] events = await EventContext.Events.FromQuery(query)
      .AsNoTracking()
      .OrderBy(e => e.Version)
      .ToArrayAsync(cancellationToken);

    return base.Load<ResourceAggregate>(events.Select(EventSerializer.Deserialize)).SingleOrDefault();
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
