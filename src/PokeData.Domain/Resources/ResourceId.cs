using Logitar.EventSourcing;

namespace PokeData.Domain.Resources;

public record ResourceId
{
  public AggregateId AggregateId { get; }

  public ResourceId(AggregateId aggregateId)
  {
    AggregateId = aggregateId;
  }

  public static ResourceId Create<T>(string identifier)
  {
    string value = string.Join(':', new[] { typeof(T).Name, identifier });
    AggregateId aggregateId = new(value);

    return new ResourceId(aggregateId);
  }
}
