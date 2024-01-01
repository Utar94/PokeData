using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal record Name
{
  [JsonPropertyName("name")]
  public string Value { get; set; } = string.Empty;

  [JsonPropertyName("language")]
  public NamedAPIResource? Language { get; set; }
}
