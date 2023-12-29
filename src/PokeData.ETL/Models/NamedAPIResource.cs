using System.Text.Json.Serialization;

namespace PokeData.ETL.Models;

internal record NamedAPIResource : APIResource
{
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;
}
