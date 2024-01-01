using Logitar.EventSourcing;
using PokeData.Domain.Regions;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class RegionEntity : AggregateEntity
{
  public int RegionId { get; private set; }

  public string UniqueName { get; private set; } = string.Empty;
  public string UniqueNameNormalized
  {
    get => UniqueName.ToUpper();
    private set { }
  }
  public string? DisplayName { get; private set; }

  public GenerationEntity? MainGeneration { get; private set; }
  public int? MainGenerationId { get; private set; }

  public List<PokemonRosterEntity> Roster { get; private set; } = [];

  public RegionEntity(RegionAggregate generation)
  {
    AggregateId = Logitar.EventSourcing.AggregateId.NewId().ToString();
    Version = 1;
    CreatedBy = UpdatedBy = ActorId.DefaultValue;
    CreatedOn = UpdatedOn = DateTime.UtcNow;

    RegionId = generation.Number;

    UniqueName = generation.UniqueName;
    DisplayName = generation.DisplayName;
  }

  private RegionEntity()
  {
  }

  public void Update(RegionAggregate generation)
  {
    if (generation.UniqueName != UniqueName || generation.DisplayName != DisplayName)
    {
      Version++;
      UpdatedBy = ActorId.DefaultValue;
      UpdatedOn = DateTime.UtcNow;

      UniqueName = generation.UniqueName;
      DisplayName = generation.DisplayName;
    }
  }
}
