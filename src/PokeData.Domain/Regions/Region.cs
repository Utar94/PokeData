using PokeData.Domain.Generations;

namespace PokeData.Domain.Regions;

public class Region
{
  public byte Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  public Generation? MainGeneration { get; set; }
}
