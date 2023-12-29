namespace PokeData.Infrastructure.Models;

internal class PokemonSpecies
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
  /// The order in which species should be sorted. Based on National Dex order, except families are grouped together and sorted by stage.
  /// </summary>
  [JsonPropertyName("order")]
  public int Order { get; set; }

  /// <summary>
  /// The chance of this Pokémon being female, in eighths; or -1 for genderless.
  /// </summary>
  [JsonPropertyName("gender_rate")]
  public int GenderRate { get; set; }

  /// <summary>
  /// The base capture rate; up to 255. The higher the number, the easier the catch.
  /// </summary>
  [JsonPropertyName("capture_rate")]
  public int CaptureRate { get; set; }

  /// <summary>
  /// The happiness when caught by a normal Pokéball; up to 255. The higher the number, the happier the Pokémon.
  /// </summary>
  [JsonPropertyName("base_happiness")]
  public int? BaseHappiness { get; set; }

  /// <summary>
  /// Whether or not this is a baby Pokémon.
  /// </summary>
  [JsonPropertyName("is_baby")]
  public bool IsBaby { get; set; }

  /// <summary>
  /// Whether or not this is a legendary Pokémon.
  /// </summary>
  [JsonPropertyName("is_legendary")]
  public bool IsLegendary { get; set; }

  /// <summary>
  /// Whether or not this is a mythical Pokémon.
  /// </summary>
  [JsonPropertyName("is_mythical")]
  public bool IsMythical { get; set; }

  /// <summary>
  /// Initial hatch counter: one must walk 255 × (hatch_counter + 1) steps before this Pokémon's egg hatches, unless utilizing bonuses like Flame Body's.
  /// </summary>
  [JsonPropertyName("hatch_counter")]
  public int? HatchCounter { get; set; }

  /// <summary>
  /// Whether or not this Pokémon has visual gender differences.
  /// </summary>
  [JsonPropertyName("has_gender_differences")]
  public bool HasGenderDifferences { get; set; }

  /// <summary>
  /// Whether or not this Pokémon has multiple forms and can switch between them.
  /// </summary>
  [JsonPropertyName("forms_switchable")]
  public bool FormsSwitchable { get; set; }

  /// <summary>
  /// The rate at which this Pokémon species gains levels.
  /// </summary>
  [JsonPropertyName("growth_rate")]
  public NamedAPIResource? GrowthRate { get; set; }

  /// <summary>
  /// A list of Pokedexes and the indexes reserved within them for this Pokémon species.
  /// </summary>
  [JsonPropertyName("pokedex_numbers")]
  public List<PokemonSpeciesDexEntry> PokedexNumbers { get; set; } = [];

  /// <summary>
  /// A list of egg groups this Pokémon species is a member of.
  /// </summary>
  [JsonPropertyName("egg_groups")]
  public List<NamedAPIResource> EggGroups { get; set; } = [];

  /// <summary>
  /// The color of this Pokémon for Pokédex search.
  /// </summary>
  [JsonPropertyName("color")]
  public NamedAPIResource? Color { get; set; }

  /// <summary>
  /// The shape of this Pokémon for Pokédex search.
  /// </summary>
  [JsonPropertyName("shape")]
  public NamedAPIResource? Shape { get; set; }

  /// <summary>
  /// The Pokémon species that evolves into this Pokemon_species.
  /// </summary>
  [JsonPropertyName("evolves_from_species")]
  public NamedAPIResource? EvolvesFromSpecies { get; set; }

  /// <summary>
  /// The evolution chain this Pokémon species is a member of.
  /// </summary>
  [JsonPropertyName("evolution_chain")]
  public APIResource? EvolutionChain { get; set; }

  /// <summary>
  /// The habitat this Pokémon species can be encountered in.
  /// </summary>
  [JsonPropertyName("habitat")]
  public NamedAPIResource? Habitat { get; set; }

  /// <summary>
  /// The generation this Pokémon species was introduced in.
  /// </summary>
  [JsonPropertyName("generation")]
  public NamedAPIResource? Generation { get; set; }

  /// <summary>
  /// The name of this resource listed in different languages.
  /// </summary>
  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  /// <summary>
  /// A list of encounters that can be had with this Pokémon species in pal park.
  /// </summary>
  [JsonPropertyName("pal_park_encounters")]
  public List<PalParkEncounterArea> PalParkEncounters { get; set; } = [];

  /// <summary>
  /// A list of flavor text entries for this Pokémon species.
  /// </summary>
  [JsonPropertyName("flavor_text_entries")]
  public List<FlavorText> FlavorTextEntries { get; set; } = [];

  /// <summary>
  /// Descriptions of different forms Pokémon take on within the Pokémon species.
  /// </summary>
  [JsonPropertyName("form_descriptions")]
  public List<Description> FormDescriptions { get; set; } = [];

  /// <summary>
  /// The genus of this Pokémon species listed in multiple languages.
  /// </summary>
  [JsonPropertyName("genera")]
  public List<Genus> Genera { get; set; } = [];

  /// <summary>
  /// A list of the Pokémon that exist within this Pokémon species.
  /// </summary>
  [JsonPropertyName("varieties")]
  public List<PokemonSpeciesVariety> Varieties { get; set; } = [];
}
