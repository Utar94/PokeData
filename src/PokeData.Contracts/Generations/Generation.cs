using PokeData.Contracts.Regions;

namespace PokeData.Contracts.Generations;

public class Generation : Aggregate
{
  public byte Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  public Region? MainRegion { get; set; }

  public override bool Equals(object? obj) => obj is Generation region && region.Number == Number;
  public override int GetHashCode() => HashCode.Combine(typeof(Generation), Number);
  public override string ToString() => $"Generation #{Number} {DisplayName ?? UniqueName}";
}
