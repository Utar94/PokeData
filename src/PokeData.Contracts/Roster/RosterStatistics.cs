namespace PokeData.Contracts.Roster;

public record RosterStatistics
{
  public ushort Count { get; set; }
  public List<RosterStatistic> Regions { get; set; } = [];
  public List<RosterStatistic> Types { get; set; } = [];
}
