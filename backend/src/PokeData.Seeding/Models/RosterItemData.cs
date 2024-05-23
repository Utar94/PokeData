using CsvHelper.Configuration.Attributes;
using Logitar;
using PokeData.Contracts.Roster;
using PokeData.Domain.Species;

namespace PokeData.Seeding.Models;

internal record RosterItemData
{
  [Index(0)]
  public ushort SpeciesId { get; set; }

  [Index(1)]
  public ushort? Number { get; set; }

  [Index(2)]
  public string? Name { get; set; }

  [Index(3)]
  public string? Category { get; set; }

  [Index(4)]
  public string? Region { get; set; }

  [Index(5)]
  public string? PrimaryType { get; set; }

  [Index(6)]
  public string? SecondaryType { get; set; }

  [Index(7)]
  public bool? IsBaby { get; set; }

  [Index(8)]
  public bool? IsLegendary { get; set; }

  [Index(9)]
  public bool? IsMythical { get; set; }

  public SaveRosterItemPayload ToRosterItemPayload(PokemonSpecies species)
  {
    if (species.Generation == null)
    {
      throw new ArgumentException($"The '{nameof(species.Generation)}' is required.", nameof(species));
    }
    else if (species.Generation.MainRegion == null)
    {
      throw new ArgumentException($"The '{nameof(species.Generation)}.{nameof(species.Generation.MainRegion)}' is required.", nameof(species));
    }

    PokemonVariety[] varieties = species.Varieties.Where(v => v.IsDefault).ToArray();
    if (varieties.Length == 0)
    {
      throw new ArgumentException($"The Pokémon species #{species.Number:D4} has no default variety.", nameof(species));
    }
    else if (varieties.Length > 1)
    {
      throw new ArgumentException($"The Pokémon species #{species.Number:D4} has multiple default varieties.", nameof(species));
    }
    PokemonVariety variety = varieties.Single();
    if (variety.PrimaryType == null)
    {
      throw new ArgumentException($"The Pokémon species #{species.Number:D4} default variety has no primary type.", nameof(species));
    }

    return new SaveRosterItemPayload
    {
      Number = Number ?? species.Number,
      Name = Name?.CleanTrim() ?? species.DisplayName ?? species.UniqueName,
      Category = IsNull(Category) ? null : (Category?.CleanTrim() ?? species.Category),
      Region = Region?.CleanTrim() ?? species.Generation.MainRegion.UniqueName,
      PrimaryType = PrimaryType?.CleanTrim() ?? variety.PrimaryType.UniqueName,
      SecondaryType = IsNull(SecondaryType) ? null : (SecondaryType?.CleanTrim() ?? variety.SecondaryType?.UniqueName),
      IsBaby = IsBaby ?? species.IsBaby,
      IsLegendary = IsLegendary ?? species.IsLegendary,
      IsMythical = IsMythical ?? species.IsMythical
    };
  }

  private static bool IsNull(string? value) => value?.Trim().Equals("<null>", StringComparison.InvariantCultureIgnoreCase) == true;
}
