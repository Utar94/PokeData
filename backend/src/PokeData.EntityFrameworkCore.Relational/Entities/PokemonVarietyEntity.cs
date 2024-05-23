using Logitar.EventSourcing;
using PokeData.Domain.Species;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class PokemonVarietyEntity : AggregateEntity
{
  public int PokemonVarietyId { get; private set; }

  public int Order { get; private set; }

  public string UniqueName { get; private set; } = string.Empty;
  public string UniqueNameNormalized
  {
    get => PokemonDb.Normalize(UniqueName);
    private set { }
  }

  public PokemonTypeEntity? PrimaryType { get; private set; }
  public int PrimaryTypeId { get; private set; }
  public PokemonTypeEntity? SecondaryType { get; private set; }
  public int? SecondaryTypeId { get; private set; }

  public double Height { get; private set; }
  public double Weight { get; private set; }

  public ushort BaseExperienceYield { get; private set; }

  public PokemonSpeciesEntity? Species { get; private set; }
  public int PokemonSpeciesId { get; private set; }
  public bool IsDefault { get; private set; }

  public PokemonVarietyEntity(PokemonVariety variety, PokemonSpeciesEntity species, PokemonTypeEntity primaryType, PokemonTypeEntity? secondaryType)
  {
    AggregateId = Logitar.EventSourcing.AggregateId.NewId().ToString();
    Version = 1;
    CreatedBy = UpdatedBy = ActorId.DefaultValue;
    CreatedOn = UpdatedOn = DateTime.UtcNow;

    PokemonVarietyId = variety.Number;

    Order = variety.Order;

    UniqueName = variety.UniqueName;

    PrimaryType = primaryType;
    PrimaryTypeId = primaryType.PokemonTypeId;
    SecondaryType = secondaryType;
    SecondaryTypeId = secondaryType?.PokemonTypeId;

    Height = variety.Height;
    Weight = variety.Weight;

    BaseExperienceYield = variety.BaseExperienceYield;

    Species = species;
    PokemonSpeciesId = species.PokemonSpeciesId;
    IsDefault = variety.IsDefault;
  }

  private PokemonVarietyEntity()
  {
  }

  public void Update(PokemonVariety variety, PokemonTypeEntity primaryType, PokemonTypeEntity? secondaryType)
  {
    if (variety.Order != Order || variety.UniqueName != UniqueName
      || primaryType.PokemonTypeId != PrimaryTypeId || secondaryType?.PokemonTypeId != SecondaryTypeId
      || variety.Height != Height || variety.Weight != Weight
      || variety.BaseExperienceYield != BaseExperienceYield || variety.IsDefault != IsDefault)
    {
      Version++;
      UpdatedBy = ActorId.DefaultValue;
      UpdatedOn = DateTime.UtcNow;

      Order = variety.Order;

      UniqueName = variety.UniqueName;

      PrimaryType = primaryType;
      PrimaryTypeId = primaryType.PokemonTypeId;
      SecondaryType = secondaryType;
      SecondaryTypeId = secondaryType?.PokemonTypeId;

      Height = variety.Height;
      Weight = variety.Weight;

      BaseExperienceYield = variety.BaseExperienceYield;

      IsDefault = variety.IsDefault;
    }
  }
}
