namespace PokeData.Contracts.Roster;

public record SaveRosterItemPayload
{
  public ushort Number { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Category { get; set; }

  public string Region { get; set; } = string.Empty;
  public string PrimaryType { get; set; } = string.Empty;
  public string? SecondaryType { get; set; }

  public bool IsBaby { get; set; }
  public bool IsLegendary { get; set; }
  public bool IsMythical { get; set; }
}
