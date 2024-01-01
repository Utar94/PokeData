using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal class PokemonSpeciesModel
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  [JsonPropertyName("order")]
  public int Order { get; set; }

  [JsonPropertyName("gender_rate")]
  public int GenderRate { get; set; }

  [JsonPropertyName("capture_rate")]
  public int CaptureRate { get; set; }

  [JsonPropertyName("base_happiness")]
  public int? BaseHappiness { get; set; }

  [JsonPropertyName("is_baby")]
  public bool IsBaby { get; set; }

  [JsonPropertyName("is_legendary")]
  public bool IsLegendary { get; set; }

  [JsonPropertyName("is_mythical")]
  public bool IsMythical { get; set; }

  [JsonPropertyName("hatch_counter")]
  public int? HatchCounter { get; set; }

  [JsonPropertyName("has_gender_differences")]
  public bool HasGenderDifferences { get; set; }

  [JsonPropertyName("forms_switchable")]
  public bool FormsSwitchable { get; set; }

  // TODO(fpion): growth_rate
  // TODO(fpion): pokedex_numbers
  // TODO(fpion): egg_groups
  // TODO(fpion): color
  // TODO(fpion): shape
  // TODO(fpion): evolves_from_species
  // TODO(fpion): evolution_chain
  // TODO(fpion): habitat

  [JsonPropertyName("generation")]
  public NamedAPIResource? Generation { get; set; }

  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  // TODO(fpion): pal_park_encounters
  // TODO(fpion): flavor_text_entries
  // TODO(fpion): form_descriptions

  [JsonPropertyName("genera")]
  public List<Genus> Genera { get; set; } = [];

  [JsonPropertyName("varieties")]
  public List<PokemonSpeciesVariety> Varieties { get; set; } = [];
}
