using System.Text.Json.Serialization;

namespace PokeData.ETL.Models;

internal record APIResource
{
  [JsonPropertyName("url")]
  public string Url { get; set; } = string.Empty;
}
