namespace PokeData.EntityFrameworkCore.Relational.Models;

internal record NamedAPIResource : APIResource // TODO(fpion): code duplication
{
  /// <summary>
  /// The name of the referenced resource.
  /// </summary>
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;
}
