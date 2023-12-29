namespace PokeData.Infrastructure.Models;

internal record Description
{
  /// <summary>
  /// The localized description for an API resource in a specific language.
  /// </summary>
  [JsonPropertyName("description")]
  public string Value { get; set; } = string.Empty;

  /// <summary>
  /// The language this name is in.
  /// </summary>
  [JsonPropertyName("language")]
  public NamedAPIResource? Language { get; set; }
}
