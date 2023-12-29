namespace PokeData.EntityFrameworkCore.Relational.Models;

internal record APIResource // TODO(fpion): code duplication
{
  /// <summary>
  /// The URL of the referenced resource.
  /// </summary>
  [JsonPropertyName("url")]
  public string Url { get; set; } = string.Empty;
}
