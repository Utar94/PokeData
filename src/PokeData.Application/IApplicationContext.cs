using Logitar.EventSourcing;

namespace PokeData.Application;

public interface IApplicationContext
{
  ActorId ActorId { get; }
}
