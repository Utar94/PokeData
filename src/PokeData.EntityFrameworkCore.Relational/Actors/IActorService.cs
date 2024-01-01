using Logitar.EventSourcing;
using PokeData.Contracts.Actors;

namespace PokeData.EntityFrameworkCore.Relational.Actors;

internal interface IActorService
{
  Task<IEnumerable<Actor>> FindAsync(IEnumerable<ActorId> ids, CancellationToken cancellationToken = default);
}
