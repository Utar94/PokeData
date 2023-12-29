namespace PokeData.Infrastructure.Models;

internal record PokemonMoveVersion
{
  /// <summary>
  /// The method by which the move is learned.
  /// </summary>
  [JsonPropertyName("move_learn_method")]
  public NamedAPIResource? LearnMethod { get; set; }

  /// <summary>
  /// The version group in which the move is learned.
  /// </summary>
  [JsonPropertyName("version_group")]
  public NamedAPIResource? VersionGroup { get; set; }

  /// <summary>
  /// The minimum level to learn the move.
  /// </summary>
  [JsonPropertyName("level_learned_at")]
  public int Level { get; set; }
}
