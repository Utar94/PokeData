namespace PokeData.Infrastructure.Models;

internal record FlavorText
{
  /// <summary>
  /// The localized flavor text for an API resource in a specific language.Note that this text is left unprocessed as it is found in game files.This means that it contains special characters that one might want to replace with their visible decodable version.Please check out this issue to find out more.
  /// </summary>
  [JsonPropertyName("flavor_text")]
  public string Value { get; set; } = string.Empty;

  /// <summary>
  /// The language this name is in.
  /// </summary>
  [JsonPropertyName("language")]
  public NamedAPIResource? Language { get; set; }

  /// <summary>
  /// The game version this flavor text is extracted from.
  /// </summary>
  [JsonPropertyName("version")]
  public NamedAPIResource? Version { get; set; }
}
