namespace PokeData.Infrastructure.Models;

internal record VersionGameIndex
{
  /// <summary>
  /// The internal id of an API resource within game data.
  /// </summary>
  [JsonPropertyName("game_index")]
  public int GameIndex { get; set; }

  /// <summary>
  /// The version relevent to this game index.
  /// </summary>
  [JsonPropertyName("version")]
  public NamedAPIResource? Version { get; set; }
}
