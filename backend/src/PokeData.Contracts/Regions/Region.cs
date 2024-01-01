using PokeData.Contracts.Generations;

namespace PokeData.Contracts.Regions;

public class Region : Aggregate
{
  public byte Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  public Generation? MainGeneration { get; set; }

  public override bool Equals(object? obj) => obj is Region region && region.Number == Number;
  public override int GetHashCode() => HashCode.Combine(typeof(Region), Number);
  public override string ToString() => $"Region #{Number} {DisplayName ?? UniqueName}";
}
