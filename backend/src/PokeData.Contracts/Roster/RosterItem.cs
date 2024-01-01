namespace PokeData.Contracts.Roster;

public class RosterItem
{
  public int SpeciesId { get; set; }
  public RosterInfo Source { get; set; } = new();
  public RosterInfo? Destination { get; set; }
}
