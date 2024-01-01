namespace PokeData.Contracts.Roster;

public record PokemonRoster
{
  public List<RosterItem> Items { get; set; } = [];
  // TODO(fpion): Stats
}
