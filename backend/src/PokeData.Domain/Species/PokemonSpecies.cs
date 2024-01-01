using PokeData.Domain.Generations;

namespace PokeData.Domain.Species;

public class PokemonSpecies
{
  public ushort Number { get; set; }
  public ushort Order { get; set; }

  public bool IsBaby { get; set; }
  public bool IsLegendary { get; set; }
  public bool IsMythical { get; set; }
  public bool HasGenderDifferences { get; set; }
  public bool CanSwitchForm { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
  public string? Category { get; set; }

  public double? GenderRatio { get; set; }
  public byte CatchRate { get; set; }
  public byte HatchTime { get; set; }

  public byte BaseFriendship { get; set; }

  public Generation? Generation { get; set; }

  public List<PokemonVariety> Varieties { get; set; } = [];
}
