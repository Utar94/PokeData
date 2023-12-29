namespace PokeData.Infrastructure.Models;

internal record PokemonType
{
  /// <summary>
  /// The order the Pokémon's types are listed in.
  /// </summary>
  [JsonPropertyName("slot")]
  public int Slot { get; set; }

  /// <summary>
  /// The type the referenced Pokémon has.
  /// </summary>
  [JsonPropertyName("type")]
  public NamedAPIResource? Type { get; set; }
}
