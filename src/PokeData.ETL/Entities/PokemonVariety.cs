using System.Text.Json.Serialization;

namespace PokeData.ETL.Entities;

internal class PokemonVariety
{
  public int Number { get; set; }
  public int Order { get; set; }

  public bool IsDefault { get; set; }

  public string UniqueName { get; set; } = string.Empty;

  public double Height { get; set; }
  public double Weight { get; set; }

  public ushort BaseExperienceYield { get; set; }

  public List<PokemonType> Types { get; set; } = [];

  [JsonIgnore]
  public PokemonSpecies? Species { get; set; }

  public static PokemonVariety FromModel(Models.Pokemon model) => new()
  {
    Number = model.Id,
    Order = model.Order,
    IsDefault = model.IsDefault,
    UniqueName = model.Name,
    Height = model.Height / 10.0,
    Weight = model.Weight / 10.0,
    BaseExperienceYield = (ushort?)model.BaseExperience ?? 0
  };
}
