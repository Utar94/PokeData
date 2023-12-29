using PokeData.Domain.Resources.Events;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class ResourceEntity : AggregateEntity
{
  public long ResourceId { get; private set; }

  public string Source { get; private set; } = string.Empty;
  public string SourceNormalized
  {
    get => Source.ToUpper();
    private set { }
  }

  public string Json { get; private set; } = string.Empty;

  public ResourceEntity(ResourceSavedEvent @event) : base(@event)
  {
    Save(@event);
  }

  private ResourceEntity() : base()
  {
  }

  public void Save(ResourceSavedEvent @event)
  {
    Update(@event);

    Source = @event.Source.Value;

    Json = @event.Json.Value;
  }
}
