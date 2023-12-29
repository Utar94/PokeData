namespace PokeData.Infrastructure.Models;

internal record Genus
{
  /// <summary>
  /// The localized genus for the referenced Pokémon species
  /// </summary>
  [JsonPropertyName("genus")]
  public string Value { get; set; } = string.Empty;

  /// <summary>
  /// The language this name is in.
  /// </summary>
  [JsonPropertyName("language")]
  public NamedAPIResource? Language { get; set; }
}
