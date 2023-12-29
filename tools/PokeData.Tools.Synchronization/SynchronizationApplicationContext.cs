using Logitar.EventSourcing;
using PokeData.Application;

namespace PokeData.Tools.Synchronization;

internal class SynchronizationApplicationContext : IApplicationContext
{
  public ActorId ActorId { get; }
}
