namespace PokeData.Infrastructure.Models;

internal record PokemonAbility
{
  /// <summary>
  /// Whether or not this is a hidden ability.
  /// </summary>
  [JsonPropertyName("is_hidden")]
  public bool IsHidden { get; set; }

  /// <summary>
  /// The slot this ability occupies in this Pokémon species.
  /// </summary>
  [JsonPropertyName("slot")]
  public int Slot { get; set; }

  /// <summary>
  /// The ability the Pokémon may have.
  /// </summary>
  [JsonPropertyName("ability")]
  public NamedAPIResource? Ability { get; set; }
}
