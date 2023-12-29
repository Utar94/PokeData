namespace PokeData.Infrastructure.Models;

internal record NamedAPIResource : APIResource
{
  /// <summary>
  /// The name of the referenced resource.
  /// </summary>
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;
}
