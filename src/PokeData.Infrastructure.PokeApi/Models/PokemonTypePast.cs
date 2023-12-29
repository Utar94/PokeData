namespace PokeData.Infrastructure.Models;

internal record PokemonTypePast
{
  /// <summary>
  /// The last generation in which the referenced pokémon had the listed types.
  /// </summary>
  [JsonPropertyName("generation")]
  public NamedAPIResource? Generation { get; set; }

  /// <summary>
  /// The types the referenced pokémon had up to and including the listed generation.
  /// </summary>
  [JsonPropertyName("types")]
  public List<PokemonType> Types { get; set; } = [];
}
