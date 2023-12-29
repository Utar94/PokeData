namespace PokeData.Infrastructure.Models;

internal class Region
{
  /// <summary>
  /// The identifier for this resource.
  /// </summary>
  [JsonPropertyName("id")]
  public int Id { get; set; }

  /// <summary>
  /// A list of locations that can be found in this region.
  /// </summary>
  [JsonPropertyName("locations")]
  public List<NamedAPIResource> Locations { get; set; } = [];

  /// <summary>
  /// The name for this resource.
  /// </summary>
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// The name of this resource listed in different languages.
  /// </summary>
  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  /// <summary>
  /// The generation this region was introduced in.
  /// </summary>
  [JsonPropertyName("main_generation")]
  public NamedAPIResource? MainGeneration { get; set; }

  /// <summary>
  /// A list of pokédexes that catalogue Pokémon in this region.
  /// </summary>
  [JsonPropertyName("pokedexes")]
  public List<NamedAPIResource> Pokedexes { get; set; } = [];

  /// <summary>
  /// A list of version groups where this region can be visited.
  /// </summary>
  [JsonPropertyName("version_groups")]
  public List<NamedAPIResource> Versions { get; set; } = [];
}
