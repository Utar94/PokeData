using PokeData.Domain.Regions;
using PokeData.Domain.Species;

namespace PokeData.Domain.Roster;

public class PokemonRoster
{
  public PokemonSpecies Species { get; set; } = new();

  public ushort Number { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Category { get; set; }

  public RegionAggregate Region { get; set; } = new();

  public PokemonType PrimaryType { get; set; } = new();
  public PokemonType? SecondaryType { get; set; }

  public bool IsBaby { get; set; }
  public bool IsLegendary { get; set; }
  public bool IsMythical { get; set; }
}
