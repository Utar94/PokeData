namespace PokeData.Infrastructure.Models;

internal class Generation
{
  /// <summary>
  /// The identifier for this resource.
  /// </summary>
  [JsonPropertyName("id")]
  public int Id { get; set; }

  /// <summary>
  /// The name for this resource.
  /// </summary>
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// A list of abilities that were introduced in this generation.
  /// </summary>
  [JsonPropertyName("abilities")]
  public List<NamedAPIResource> Abilities { get; set; } = [];

  /// <summary>
  /// The name of this resource listed in different languages.
  /// </summary>
  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  /// <summary>
  /// The main region travelled in this generation.
  /// </summary>
  [JsonPropertyName("main_region")]
  public NamedAPIResource? MainRegion { get; set; }

  /// <summary>
  /// A list of moves that were introduced in this generation.
  /// </summary>
  [JsonPropertyName("moves")]
  public List<NamedAPIResource> Moves { get; set; } = [];

  /// <summary>
  /// A list of Pokémon species that were introduced in this generation.
  /// </summary>
  [JsonPropertyName("pokemon_species")]
  public List<NamedAPIResource> Species { get; set; } = [];

  /// <summary>
  /// A list of types that were introduced in this generation.
  /// </summary>
  [JsonPropertyName("types")]
  public List<NamedAPIResource> Types { get; set; } = [];

  /// <summary>
  /// A list of version groups that were introduced in this generation.
  /// </summary>
  [JsonPropertyName("version_groups")]
  public List<NamedAPIResource> Versions { get; set; } = [];
}
