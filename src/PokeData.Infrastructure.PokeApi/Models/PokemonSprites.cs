namespace PokeData.Infrastructure.Models;

internal record PokemonSprites
{
  /// <summary>
  /// The default depiction of this Pokémon from the front in battle.
  /// </summary>
  [JsonPropertyName("front_default")]
  public string FrontDefault { get; set; } = string.Empty;

  /// <summary>
  /// The shiny depiction of this Pokémon from the front in battle.
  /// </summary>
  [JsonPropertyName("front_shiny")]
  public string FrontShiny { get; set; } = string.Empty;

  /// <summary>
  /// The female depiction of this Pokémon from the front in battle.
  /// </summary>
  [JsonPropertyName("front_female")]
  public string FrontFemale { get; set; } = string.Empty;

  /// <summary>
  /// The shiny female depiction of this Pokémon from the front in battle.
  /// </summary>
  [JsonPropertyName("front_shiny_female")]
  public string FrontShinyFemale { get; set; } = string.Empty;

  /// <summary>
  /// The default depiction of this Pokémon from the back in battle.
  /// </summary>
  [JsonPropertyName("back_default")]
  public string BackDefault { get; set; } = string.Empty;

  /// <summary>
  /// The shiny depiction of this Pokémon from the back in battle.
  /// </summary>
  [JsonPropertyName("back_shiny")]
  public string BackShiny { get; set; } = string.Empty;

  /// <summary>
  /// The female depiction of this Pokémon from the back in battle.
  /// </summary>
  [JsonPropertyName("back_female")]
  public string BackFemale { get; set; } = string.Empty;

  /// <summary>
  /// The shiny female depiction of this Pokémon from the back in battle.
  /// </summary>
  [JsonPropertyName("back_shiny_female")]
  public string BackShinyFemale { get; set; } = string.Empty;
}
