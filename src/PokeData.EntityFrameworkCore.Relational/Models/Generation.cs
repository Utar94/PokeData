namespace PokeData.EntityFrameworkCore.Relational.Models;

internal class Generation // TODO(fpion): code duplication
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
  public string UniqueName { get; set; } = string.Empty;

  /// <summary>
  /// The name of this resource listed in different languages.
  /// </summary>
  [JsonPropertyName("names")]
  public List<Name> DisplayNames { get; set; } = [];
}
