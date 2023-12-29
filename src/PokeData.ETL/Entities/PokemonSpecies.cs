namespace PokeData.ETL.Entities;

internal class PokemonSpecies
{
  public ushort Number { get; set; }
  public int Order { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
  public string? Category { get; set; }

  public double? GenderRatio { get; set; }
  public byte CatchRate { get; set; }

  public byte HatchTime { get; set; }

  public byte BaseFriendship { get; set; }

  public bool IsBaby { get; set; }
  public bool IsLegendary { get; set; }
  public bool IsMythical { get; set; }
  public bool HasGenderDifferences { get; set; }
  public bool CanSwitchForm { get; set; }

  public Generation? Generation { get; set; }
  public List<PokemonVariety> Varieties { get; set; } = [];

  public static PokemonSpecies FromModel(Models.PokemonSpecies model, string languageName) => new()
  {
    Number = (ushort)model.Id,
    Order = model.Order,
    UniqueName = model.Name,
    DisplayName = model.Names.SingleOrDefault(name => name.Language?.Name == languageName)?.Value,
    Category = model.Genera.SingleOrDefault(genus => genus.Language?.Name == languageName)?.Value,
    GenderRatio = model.GenderRate == -1 ? null : (1 - model.GenderRate / 8.0),
    CatchRate = (byte)model.CaptureRate,
    HatchTime = (byte?)model.HatchCounter ?? 0,
    BaseFriendship = (byte?)model.BaseHappiness ?? 0,
    IsBaby = model.IsBaby,
    IsLegendary = model.IsLegendary,
    IsMythical = model.IsMythical,
    HasGenderDifferences = model.HasGenderDifferences,
    CanSwitchForm = model.FormsSwitchable
  };
}
