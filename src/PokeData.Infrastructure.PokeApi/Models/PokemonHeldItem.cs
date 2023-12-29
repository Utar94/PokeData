namespace PokeData.Infrastructure.Models;

internal record PokemonHeldItem
{
  /// <summary>
  /// The item the referenced Pokémon holds.
  /// </summary>
  [JsonPropertyName("item")]
  public NamedAPIResource? Item { get; set; }

  /// <summary>
  /// The details of the different versions in which the item is held.
  /// </summary>
  [JsonPropertyName("version_details")]
  public List<PokemonHeldItemVersion> VersionDetails { get; set; } = [];
}
