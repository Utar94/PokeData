using PokeData.Contracts.Roster;

namespace PokeData.Application.Roster;

public interface IPokemonRosterQuerier
{
  Task<PokemonRoster> ReadAsync(CancellationToken cancellationToken = default);
}
