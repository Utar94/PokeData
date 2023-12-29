using System.Text.Json.Serialization;

namespace PokeData.ETL.Entities;

internal class Region
{
  public int Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  [JsonIgnore]
  public Generation? Generation { get; set; }

  public static Region FromModel(Models.Region model, string languageName) => new()
  {
    Number = model.Id,
    UniqueName = model.Name,
    DisplayName = model.Names.SingleOrDefault(name => name.Language?.Name == languageName)?.Value
  };
}
