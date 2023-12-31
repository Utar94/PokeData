using Logitar.EventSourcing;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal abstract class AggregateEntity
{
  public string AggregateId { get; protected set; } = string.Empty;
  public long Version { get; protected set; }

  public string CreatedBy { get; protected set; } = string.Empty;
  public DateTime CreatedOn { get; protected set; }

  public string UpdatedBy { get; protected set; } = string.Empty;
  public DateTime UpdatedOn { get; protected set; }

  protected AggregateEntity()
  {
  }

  protected AggregateEntity(DomainEvent @event)
  {
    AggregateId = @event.AggregateId.Value;

    CreatedBy = @event.ActorId.Value;
    CreatedOn = @event.OccurredOn.ToUniversalTime();

    Update(@event);
  }

  protected void Update(DomainEvent @event)
  {
    Version = @event.Version;

    UpdatedBy = @event.ActorId.Value;
    UpdatedOn = @event.OccurredOn.ToUniversalTime();
  }
}
