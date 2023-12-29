namespace PokeData.Infrastructure.Models;

internal record PokemonMove
{
  /// <summary>
  /// The move the Pokémon can learn.
  /// </summary>
  [JsonPropertyName("move")]
  public NamedAPIResource? Move { get; set; }

  /// <summary>
  /// The details of the version in which the Pokémon can learn the move.
  /// </summary>
  [JsonPropertyName("version_group_details")]
  public List<PokemonMoveVersion> Versions { get; set; } = [];
}
