namespace PokeData.Contracts.Roster;

public record PokemonRoster
{
  public List<RosterItem> Items { get; set; } = [];
  public RosterStatistics Stats { get; set; } = new();
}
