using Logitar.EventSourcing;

namespace PokeData.Domain.Resources;

public record ResourceId
{
  private const char Separator = ':';

  public AggregateId AggregateId { get; }

  public string Type { get; }
  public string Identifier { get; }

  public ResourceId(AggregateId aggregateId)
  {
    AggregateId = aggregateId;

    string[] parts = aggregateId.Value.Split(Separator);
    if (parts.Length != 2)
    {
      throw new ArgumentException($"The value '{aggregateId}' is not a valid resource identifier.", nameof(aggregateId));
    }
    Type = parts[0];
    Identifier = parts[1];
  }

  private ResourceId(string type, string identifier)
  {
    AggregateId = new(string.Join(Separator, type, identifier));
    Type = type;
    Identifier = identifier;
  }

  public static ResourceId Create<T>(string identifier) => new(typeof(T).Name, identifier);
}
