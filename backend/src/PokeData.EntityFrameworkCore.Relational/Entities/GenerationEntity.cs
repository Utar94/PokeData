using Logitar.EventSourcing;
using PokeData.Domain.Generations;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class GenerationEntity : AggregateEntity
{
  public int GenerationId { get; private set; }

  public string UniqueName { get; private set; } = string.Empty;
  public string UniqueNameNormalized
  {
    get => PokemonDb.Normalize(UniqueName);
    private set { }
  }
  public string? DisplayName { get; private set; }

  public RegionEntity? MainRegion { get; private set; }
  public int MainRegionId { get; private set; }

  public List<PokemonSpeciesEntity> Species { get; private set; } = [];

  public GenerationEntity(Generation generation, RegionEntity mainRegion)
  {
    AggregateId = Logitar.EventSourcing.AggregateId.NewId().ToString();
    Version = 1;
    CreatedBy = UpdatedBy = ActorId.DefaultValue;
    CreatedOn = UpdatedOn = DateTime.UtcNow;

    GenerationId = generation.Number;

    UniqueName = generation.UniqueName;
    DisplayName = generation.DisplayName;

    MainRegion = mainRegion;
    MainRegionId = mainRegion.RegionId;

  }

  private GenerationEntity()
  {
  }

  public void Update(Generation generation)
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
