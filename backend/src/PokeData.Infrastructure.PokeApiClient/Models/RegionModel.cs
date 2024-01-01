using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal class RegionModel
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("locations")]
  public List<NamedAPIResource> Locations { get; set; } = [];

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  [JsonPropertyName("main_generation")]
  public NamedAPIResource? MainGeneration { get; set; }

  [JsonPropertyName("pokedexes")]
  public List<NamedAPIResource> Pokedexes { get; set; } = [];

  [JsonPropertyName("version_groups")]
  public List<NamedAPIResource> VersionGroups { get; set; } = [];
}
