using Logitar.EventSourcing;
using Microsoft.Extensions.Caching.Memory;
using PokeData.Application.Caching;
using PokeData.Contracts.Actors;
using PokeData.Domain.Resources;
using PokeData.Infrastructure.Settings;

namespace PokeData.Infrastructure.Caching;

internal class CacheService : ICacheService
{
  private readonly IMemoryCache _cache;
  private readonly CachingSettings _settings;

  public CacheService(IMemoryCache cache, CachingSettings settings)
  {
    _cache = cache;
    _settings = settings;
  }

  public Actor? GetActor(ActorId id) => GetItem<Actor>(GetActorKey(id));
  public void SetActor(Actor actor)
  {
    TimeSpan? expiration = _settings.ActorLifetimeSeconds > 0 ? TimeSpan.FromSeconds(_settings.ActorLifetimeSeconds) : null;
    ActorId id = new(actor.Id);
    SetItem(GetActorKey(id), actor, expiration);
  }
  private static string GetActorKey(ActorId id) => $"Actor.Id:{id}";

  public Resource? GetResource(Uri source) => GetItem<Resource>(GetResourceKey(source));
  public void SetResource(Resource resource)
  {
    TimeSpan? expiration = _settings.ResourceLifetimeSeconds > 0 ? TimeSpan.FromSeconds(_settings.ResourceLifetimeSeconds) : null;
    SetItem(GetResourceKey(resource.Source), resource, expiration);
  }
  private static string GetResourceKey(Uri source) => $"Resource.Source|{source.ToString().ToUpper()}";

  private T? GetItem<T>(object key) => _cache.TryGetValue(key, out object? value) ? (T?)value : default;
  private void SetItem<T>(object key, T value, TimeSpan? expiration = null)
  {
    if (expiration.HasValue)
    {
      _cache.Set(key, value, expiration.Value);
    }
    else
    {
      _cache.Set(key, value);
    }
  }
}
