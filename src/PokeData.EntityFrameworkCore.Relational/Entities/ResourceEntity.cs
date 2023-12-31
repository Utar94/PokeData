using Logitar.EventSourcing;
using PokeData.Domain.Resources;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class ResourceEntity : AggregateEntity
{
  public int ResourceId { get; private set; }

  public string Source { get; private set; } = string.Empty;
  public string SourceNormalized
  {
    get => Source.ToUpper();
    private set { }
  }

  public string ContentType { get; private set; } = string.Empty;
  public string ContentText { get; private set; } = string.Empty;

  public ResourceEntity(Resource resource) : base()
  {
    AggregateId = Logitar.EventSourcing.AggregateId.NewId().ToString();
    Version = 1;
    CreatedBy = UpdatedBy = ActorId.DefaultValue;
    CreatedOn = UpdatedOn = DateTime.UtcNow;

    Source = resource.Source.ToString();

    ContentType = resource.Content.Type;
    ContentText = resource.Content.Text;
  }

  private ResourceEntity() : base()
  {
  }

  public void Update(Resource resource)
  {
    if (resource.Content.Type != ContentType || resource.Content.Text != ContentText)
    {
      Version++;
      UpdatedBy = ActorId.DefaultValue;
      UpdatedOn = DateTime.UtcNow;

      ContentType = resource.Content.Type;
      ContentText = resource.Content.Text;
    }
  }
}
