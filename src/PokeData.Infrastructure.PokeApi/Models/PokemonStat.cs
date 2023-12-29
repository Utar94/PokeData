namespace PokeData.Infrastructure.Models;

internal record PokemonStat
{
  /// <summary>
  /// The stat the Pokémon has.
  /// </summary>
  [JsonPropertyName("stat")]
  public NamedAPIResource? Stat { get; set; }

  /// <summary>
  /// The effort points(EV) the Pokémon has in the stat.
  /// </summary>
  [JsonPropertyName("effort")]
  public int Effort { get; set; }

  /// <summary>
  /// The base value of the stat.
  /// </summary>
  [JsonPropertyName("base_stat")]
  public int BaseStat { get; set; }
}
