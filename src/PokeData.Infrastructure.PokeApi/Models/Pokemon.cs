namespace PokeData.Infrastructure.Models;

internal class Pokemon
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
  /// The base experience gained for defeating this Pokémon.
  /// </summary>
  [JsonPropertyName("base_experience")]
  public int BaseExperience { get; set; }

  /// <summary>
  /// The height of this Pokémon in decimetres.
  /// </summary>
  [JsonPropertyName("height")]
  public int Height { get; set; }

  /// <summary>
  /// Set for exactly one Pokémon used as the default for each species.
  /// </summary>
  [JsonPropertyName("is_default")]
  public bool IsDefault { get; set; }

  /// <summary>
  /// Order for sorting. Almost national order, except families are grouped together.
  /// </summary>
  [JsonPropertyName("order")]
  public int Order { get; set; }

  /// <summary>
  /// The weight of this Pokémon in hectograms.
  /// </summary>
  [JsonPropertyName("weight")]
  public int Weight { get; set; }

  /// <summary>
  /// A list of abilities this Pokémon could potentially have.
  /// </summary>
  [JsonPropertyName("abilities")]
  public List<PokemonAbility> Abilities { get; set; } = [];

  /// <summary>
  /// A list of forms this Pokémon can take on.
  /// </summary>
  [JsonPropertyName("forms")]
  public List<NamedAPIResource> Forms { get; set; } = [];

  /// <summary>
  /// A list of game indices relevent to Pokémon item by generation.
  /// </summary>
  [JsonPropertyName("game_indices")]
  public List<VersionGameIndex> GameIndices { get; set; } = [];

  /// <summary>
  /// A list of items this Pokémon may be holding when encountered.
  /// </summary>
  [JsonPropertyName("held_items")]
  public List<PokemonHeldItem> HeldItems { get; set; } = [];

  /// <summary>
  /// A link to a list of location areas, as well as encounter details pertaining to specific versions.
  /// </summary>
  public string LocationAreaEncounters { get; set; } = string.Empty;

  /// <summary>
  /// A list of moves along with learn methods and level details pertaining to specific version groups.
  /// </summary>
  [JsonPropertyName("moves")]
  public List<PokemonMove> Moves { get; set; } = [];

  /// <summary>
  /// A list of details showing types this pokémon had in previous generations.
  /// </summary>
  [JsonPropertyName("past_types")]
  public List<PokemonTypePast> PastTypes { get; set; } = [];

  /// <summary>
  /// 
  /// </summary>
  [JsonPropertyName("sprites")]
  public PokemonSprites? Sprites { get; set; }

  /// <summary>
  /// The species this Pokémon belongs to.
  /// </summary>
  [JsonPropertyName("species")]
  public NamedAPIResource? Species { get; set; }

  /// <summary>
  /// A list of base stat values for this Pokémon.
  /// </summary>
  [JsonPropertyName("stats")]
  public List<PokemonStat> Stats { get; set; } = [];

  /// <summary>
  /// A list of details showing types this Pokémon has.
  /// </summary>
  [JsonPropertyName("types")]
  public List<PokemonType> Types { get; set; } = [];
}
