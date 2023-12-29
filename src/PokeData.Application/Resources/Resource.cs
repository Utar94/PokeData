using PokeData.Domain.Resources;

namespace PokeData.Application.Resources;

public class Resource
{
  public ResourceId Id { get; }
  public SourceUnit Source { get; }
  public JsonStringUnit Json { get; }

  public Resource(ResourceId id, SourceUnit source, JsonStringUnit json)
  {
    Id = id;
    Source = source;
    Json = json;
  }

  public override bool Equals(object? obj) => obj is Resource resource && resource.Id.Equals(Id);
  public override int GetHashCode() => Id.AggregateId.GetHashCode();
  public override string ToString() => Id.AggregateId.Value;
}
