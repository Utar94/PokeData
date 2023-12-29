namespace PokeData.Infrastructure.Models;

internal record PokemonHeldItemVersion
{
  /// <summary>
  /// The version in which the item is held.
  /// </summary>
  [JsonPropertyName("version")]
  public NamedAPIResource? Version { get; set; }

  /// <summary>
  /// How often the item is held.
  /// </summary>
  [JsonPropertyName("rarity")]
  public int Rarity { get; set; }
}
