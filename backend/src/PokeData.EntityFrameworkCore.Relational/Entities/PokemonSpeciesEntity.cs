using Logitar.EventSourcing;
using PokeData.Domain.Species;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class PokemonSpeciesEntity : AggregateEntity
{
  public int PokemonSpeciesId { get; private set; }

  public GenerationEntity? Generation { get; private set; }
  public int GenerationId { get; private set; }

  public int Order { get; private set; }

  public bool IsBaby { get; private set; }
  public bool IsLegendary { get; private set; }
  public bool IsMythical { get; private set; }
  public bool HasGenderDifferences { get; private set; }
  public bool CanSwitchForm { get; private set; }

  public string UniqueName { get; private set; } = string.Empty;
  public string UniqueNameNormalized
  {
    get => PokemonDb.Normalize(UniqueName);
    private set { }
  }
  public string? DisplayName { get; private set; }
  public string? Category { get; private set; }

  public double? GenderRatio { get; private set; }
  public byte CatchRate { get; private set; }
  public byte HatchTime { get; private set; }

  public byte BaseFriendship { get; private set; }

  public PokemonRosterEntity? Roster { get; private set; }
  public List<PokemonVarietyEntity> Varieties { get; private set; } = [];

  public PokemonSpeciesEntity(PokemonSpecies species, GenerationEntity generation)
  {
    AggregateId = Logitar.EventSourcing.AggregateId.NewId().ToString();
    Version = 1;
    CreatedBy = UpdatedBy = ActorId.DefaultValue;
    CreatedOn = UpdatedOn = DateTime.UtcNow;

    PokemonSpeciesId = species.Number;

    Generation = generation;
    GenerationId = generation.GenerationId;

    Order = species.Order;

    IsBaby = species.IsBaby;
    IsLegendary = species.IsLegendary;
    IsMythical = species.IsMythical;
    HasGenderDifferences = species.HasGenderDifferences;
    CanSwitchForm = species.CanSwitchForm;

    UniqueName = species.UniqueName;
    DisplayName = species.DisplayName;
    Category = species.Category;

    GenderRatio = species.GenderRatio;
    CatchRate = species.CatchRate;
    HatchTime = species.HatchTime;

    BaseFriendship = species.BaseFriendship;
  }

  private PokemonSpeciesEntity()
  {
  }

  public void Update(PokemonSpecies species, GenerationEntity generation)
  {
    if (generation.GenerationId != GenerationId || species.Order != Order
      || species.IsBaby != IsBaby || species.IsLegendary != IsLegendary || species.IsMythical != IsMythical
      || species.HasGenderDifferences != HasGenderDifferences || species.CanSwitchForm != CanSwitchForm
      || species.UniqueName != UniqueName || species.DisplayName != DisplayName || species.Category != Category
      || species.GenderRatio != GenderRatio || species.CatchRate != CatchRate || species.HatchTime != HatchTime
      || species.BaseFriendship != BaseFriendship)
    {
      Version++;
      UpdatedBy = ActorId.DefaultValue;
      UpdatedOn = DateTime.UtcNow;

      Generation = generation;
      GenerationId = generation.GenerationId;

      Order = species.Order;

      IsBaby = species.IsBaby;
      IsLegendary = species.IsLegendary;
      IsMythical = species.IsMythical;
      HasGenderDifferences = species.HasGenderDifferences;
      CanSwitchForm = species.CanSwitchForm;

      UniqueName = species.UniqueName;
      DisplayName = species.DisplayName;
      Category = species.Category;

      GenderRatio = species.GenderRatio;
      CatchRate = species.CatchRate;
      HatchTime = species.HatchTime;

      BaseFriendship = species.BaseFriendship;
    }
  }
}
