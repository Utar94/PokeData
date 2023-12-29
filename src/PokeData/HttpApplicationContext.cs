using Logitar.EventSourcing;
using PokeData.Application;

namespace PokeData;

internal class HttpApplicationContext : IApplicationContext
{
  public ActorId ActorId { get; } = new(); // TODO(fpion): Authentication
}
