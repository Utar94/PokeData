using PokeData.Infrastructure.Models;

namespace PokeData.Infrastructure.PokeApi.Models;

internal record Type
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

  // TODO(fpion): damage_relations

  // TODO(fpion): past_damage_relations

  // TODO(fpion): game_indices

  /// <summary>
  /// The generation this type was introduced in.
  /// </summary>
  [JsonPropertyName("generation")]
  public NamedAPIResource? Generation { get; set; }

  // TODO(fpion): move_damage_class

  /// <summary>
  /// The name of this resource listed in different languages.
  /// </summary>
  [JsonPropertyName("names")]
  public List<Name> Names { get; set; } = [];

  // TODO(fpion): pokemon

  // TODO(fpion): moves
}
