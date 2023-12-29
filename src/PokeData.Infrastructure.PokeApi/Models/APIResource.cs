namespace PokeData.Infrastructure.Models;

internal record APIResource
{
  /// <summary>
  /// The URL of the referenced resource.
  /// </summary>
  [JsonPropertyName("url")]
  public string Url { get; set; } = string.Empty;
}
