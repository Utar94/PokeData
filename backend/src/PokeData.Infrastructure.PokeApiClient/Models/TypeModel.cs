using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal class TypeModel
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  // TODO(fpion): damage_relations
  // TODO(fpion): past_damage_relations
  // TODO(fpion): game_indices

  [JsonPropertyName("generation")]
  public NamedAPIResource? Generation { get; set; }

  // TODO(fpion): move_damage_class

  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  // TODO(fpion): pokemon
  // TODO(fpion): moves
}
