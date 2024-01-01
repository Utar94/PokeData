using Logitar.EventSourcing;
using PokeData.Domain.Roster;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class PokemonRosterEntity : AggregateEntity
{
  public PokemonSpeciesEntity? PokemonSpecies { get; private set; }
  public int PokemonSpeciesId { get; private set; }

  public int Number { get; private set; }
  public string Name { get; private set; } = string.Empty;
  public string? Category { get; private set; }

  public RegionEntity? Region { get; private set; }
  public int RegionId { get; private set; }

  public PokemonTypeEntity? PrimaryType { get; private set; }
  public int PrimaryTypeId { get; private set; }
  public PokemonTypeEntity? SecondaryType { get; private set; }
  public int? SecondaryTypeId { get; private set; }

  public bool IsBaby { get; private set; }
  public bool IsLegendary { get; private set; }
  public bool IsMythical { get; private set; }

  public PokemonRosterEntity(PokemonRoster roster)
  {
    AggregateId = Logitar.EventSourcing.AggregateId.NewId().ToString();
    Version = 1;
    CreatedBy = UpdatedBy = ActorId.DefaultValue;
    CreatedOn = UpdatedOn = DateTime.UtcNow;

    PokemonSpeciesId = roster.Species.Number;

    Number = roster.Number;
    Name = roster.Name;
    Category = roster.Category;

    RegionId = roster.Region.Number;

    PrimaryTypeId = roster.PrimaryType.Number;
    SecondaryTypeId = roster.SecondaryType?.Number;

    IsBaby = roster.IsBaby;
    IsLegendary = roster.IsLegendary;
    IsMythical = roster.IsMythical;
  }

  private PokemonRosterEntity()
  {
  }

  public void Update(PokemonRoster roster)
  {
    if (roster.Number != Number || roster.Name != Name || roster.Category != Category
      || roster.Region.Number != RegionId || roster.PrimaryType.Number != PrimaryTypeId || roster.SecondaryType?.Number != SecondaryTypeId
      || roster.IsBaby != IsBaby || roster.IsLegendary != IsLegendary || roster.IsMythical != IsMythical)
    {
      Version++;
      UpdatedBy = ActorId.DefaultValue;
      UpdatedOn = DateTime.UtcNow;

      Number = roster.Number;
      Name = roster.Name;
      Category = roster.Category;

      RegionId = roster.Region.Number;

      PrimaryTypeId = roster.PrimaryType.Number;
      SecondaryTypeId = roster.SecondaryType?.Number;

      IsBaby = roster.IsBaby;
      IsLegendary = roster.IsLegendary;
      IsMythical = roster.IsMythical;
    }
  }
}
