namespace PokeData.Infrastructure.Models;

internal record PalParkEncounterArea
{
  /// <summary>
  /// The base score given to the player when the referenced Pokémon is caught during a pal park run.
  /// </summary>
  [JsonPropertyName("base_score")]
  public int BaseScore { get; set; }

  /// <summary>
  /// The base rate for encountering the referenced Pokémon in this pal park area.
  /// </summary>
  [JsonPropertyName("rate")]
  public int Rate { get; set; }

  /// <summary>
  /// The pal park area where this encounter happens.
  /// </summary>
  [JsonPropertyName("area")]
  public NamedAPIResource? Area { get; set; }
}
