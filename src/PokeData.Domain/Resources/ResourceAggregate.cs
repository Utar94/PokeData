using Logitar.EventSourcing;
using PokeData.Domain.Resources.Events;

namespace PokeData.Domain.Resources;

public class ResourceAggregate : AggregateRoot
{
  public new ResourceId Id => new(base.Id);

  private SourceUnit? _source = null;
  public SourceUnit Source => _source ?? throw new InvalidOperationException("The source has not been initialized yet.");

  private JsonStringUnit? _json = null;
  public JsonStringUnit Json => _json ?? throw new InvalidOperationException("The JSON has not been initialized yet.");

  public ResourceAggregate(AggregateId id) : base(id)
  {
  }

  public ResourceAggregate(ResourceId id, SourceUnit source, JsonStringUnit json, ActorId actorId = default) : base(id.AggregateId)
  {
    Raise(new ResourceSavedEvent(source, json, actorId));
  }

  public void Update(SourceUnit source, JsonStringUnit json, ActorId actorId = default)
  {
    if (source != Source || json != Json)
    {
      Raise(new ResourceSavedEvent(source, json, actorId));
    }
  }

  protected virtual void Apply(ResourceSavedEvent @event)
  {
    _source = @event.Source;

    _json = @event.Json;
  }
}
