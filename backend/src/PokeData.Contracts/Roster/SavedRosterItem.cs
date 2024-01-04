namespace PokeData.Contracts.Roster;

public record SavedRosterItem
{
  public RosterItem Item { get; set; } = new();
  public RosterStatistics Stats { get; set; } = new();
}
