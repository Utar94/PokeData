namespace PokeData.Contracts.Types;

public class PokemonType : Aggregate
{
  public byte Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  public override bool Equals(object? obj) => obj is PokemonType region && region.Number == Number;
  public override int GetHashCode() => HashCode.Combine(typeof(PokemonType), Number);
  public override string ToString() => $"Type #{Number} {DisplayName ?? UniqueName}";
}
