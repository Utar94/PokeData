using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal class Pokemon
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  [JsonPropertyName("base_experience")]
  public int? BaseExperience { get; set; }

  [JsonPropertyName("height")]
  public int Height { get; set; }

  [JsonPropertyName("is_default")]
  public bool IsDefault { get; set; }

  [JsonPropertyName("order")]
  public int Order { get; set; }

  [JsonPropertyName("weight")]
  public int Weight { get; set; }

  // TODO(fpion): abilities
  // TODO(fpion): forms
  // TODO(fpion): game_indices
  // TODO(fpion): held_items
  // TODO(fpion): location_area_encounters
  // TODO(fpion): moves
  // TODO(fpion): past_types
  // TODO(fpion): sprites

  [JsonPropertyName("species")]
  public NamedAPIResource? Species { get; set; }

  // TODO(fpion): stats

  [JsonPropertyName("types")]
  public List<PokemonTypeModel> Types { get; set; } = [];
}
