using Logitar.EventSourcing;
using PokeData.Contracts.Actors;
using PokeData.Domain.Resources;

namespace PokeData.Application.Caching;

public interface ICacheService
{
  Actor? GetActor(ActorId id);
  void SetActor(Actor actor);

  Resource? GetResource(Uri source);
  void SetResource(Resource resource);
}
