namespace PokeData.Infrastructure.Models;

internal record PokemonSpeciesDexEntry
{
  /// <summary>
  /// The index number within the Pokédex.
  /// </summary>
  [JsonPropertyName("entry_number")]
  public int EntryNumber { get; set; }

  /// <summary>
  /// The Pokédex the referenced Pokémon species can be found in.
  /// </summary>
  [JsonPropertyName("pokedex")]
  public NamedAPIResource? Pokedex { get; set; }
}
