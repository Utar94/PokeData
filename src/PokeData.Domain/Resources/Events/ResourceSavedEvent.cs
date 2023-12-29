using Logitar.EventSourcing;
using MediatR;

namespace PokeData.Domain.Resources.Events;

public record ResourceSavedEvent : DomainEvent, INotification
{
  public SourceUnit Source { get; }

  public JsonStringUnit Json { get; }

  public ResourceSavedEvent(SourceUnit source, JsonStringUnit json, ActorId actorId)
  {
    ActorId = actorId;
    Source = source;
    Json = json;
  }
}
