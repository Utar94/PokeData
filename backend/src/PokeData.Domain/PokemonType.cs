namespace PokeData.Domain;

public class PokemonType
{
  public byte Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
}
