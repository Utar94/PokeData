namespace PokeData.EntityFrameworkCore.Relational.Models;

internal record Name // TODO(fpion): code duplication
{
  /// <summary>
  /// The localized name for an API resource in a specific language.
  /// </summary>
  [JsonPropertyName("name")]
  public string Value { get; set; } = string.Empty;

  /// <summary>
  /// The language this name is in.
  /// </summary>
  [JsonPropertyName("language")]
  public NamedAPIResource? Language { get; set; }
}
