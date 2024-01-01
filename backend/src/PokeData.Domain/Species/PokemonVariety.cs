namespace PokeData.Domain.Species;

public class PokemonVariety
{
  public ushort Number { get; set; }
  public ushort Order { get; set; }

  public string UniqueName { get; set; } = string.Empty;

  public PokemonType? PrimaryType { get; set; }
  public PokemonType? SecondaryType { get; set; }

  public double Height { get; set; }
  public double Weight { get; set; }

  public ushort BaseExperienceYield { get; set; }

  public PokemonSpecies? Species { get; set; }
  public bool IsDefault { get; set; }
}
