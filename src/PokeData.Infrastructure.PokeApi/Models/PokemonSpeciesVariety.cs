namespace PokeData.Infrastructure.Models;

internal record PokemonSpeciesVariety
{
  /// <summary>
  /// Whether this variety is the default variety.
  /// </summary>
  [JsonPropertyName("is_default")]
  public bool IsDefault { get; set; }

  /// <summary>
  /// The Pokémon variety.
  /// </summary>
  [JsonPropertyName("pokemon")]
  public NamedAPIResource? Pokemon { get; set; }
}
