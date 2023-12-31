using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal record NamedAPIResource : APIResource
{
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;
}
