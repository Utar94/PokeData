using System.Text.Json.Serialization;

namespace PokeData.ETL.Entities;

internal class Generation
{
  public int Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  public Region? Region { get; set; }

  [JsonIgnore]
  public List<PokemonSpecies> Species { get; set; } = [];

  public static Generation FromModel(Models.Generation model, string languageName) => new()
  {
    Number = model.Id,
    UniqueName = model.Name,
    DisplayName = model.Names.SingleOrDefault(name => name.Language?.Name == languageName)?.Value
  };
}
