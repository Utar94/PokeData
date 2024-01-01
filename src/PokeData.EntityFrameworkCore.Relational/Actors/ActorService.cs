using Logitar.EventSourcing;
using Microsoft.EntityFrameworkCore;
using PokeData.Application.Caching;
using PokeData.Contracts.Actors;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Actors;

internal class ActorService : IActorService
{
  private readonly DbSet<ActorEntity> _actors;
  private readonly ICacheService _cacheService;

  public ActorService(ICacheService cacheService, PokemonContext context)
  {
    _actors = context.Actors;
    _cacheService = cacheService;
  }

  public async Task<IEnumerable<Actor>> FindAsync(IEnumerable<ActorId> ids, CancellationToken cancellationToken)
  {
    Dictionary<ActorId, Actor> actors = new(capacity: ids.Count());
    HashSet<string> missingIds = new(capacity: actors.Count);

    foreach (ActorId id in ids)
    {
      if (id.Value == ActorId.DefaultValue)
      {
        continue;
      }

      Actor? actor = _cacheService.GetActor(id);
      if (actor == null)
      {
        missingIds.Add(id.Value);
      }
      else
      {
        actors[id] = actor;
        _cacheService.SetActor(actor);
      }
    }

    if (missingIds.Count > 0)
    {
      Mapper mapper = new();

      ActorEntity[] entities = await _actors.AsNoTracking()
        .Where(a => missingIds.Contains(a.Id))
        .ToArrayAsync(cancellationToken);
      foreach (ActorEntity entity in entities)
      {
        Actor actor = mapper.ToActor(entity);
        _cacheService.SetActor(actor);

        ActorId id = new(actor.Id);
        actors[id] = actor;
      }
    }

    return actors.Values;
  }
}
