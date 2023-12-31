using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal record APIResource
{
  [JsonPropertyName("url")]
  public string Url { get; set; } = string.Empty;
}
