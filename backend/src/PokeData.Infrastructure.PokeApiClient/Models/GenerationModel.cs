using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal record GenerationModel
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  // TODO(fpion): abilities

  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  [JsonPropertyName("main_region")]
  public NamedAPIResource? MainRegion { get; set; }

  // TODO(fpion): moves
  // TODO(fpion): pokemon_species
  // TODO(fpion): types
  // TODO(fpion): version_groups
}
