namespace PokeData.Domain.Roster;

public interface IPokemonRosterRepository
{
  Task SaveAsync(PokemonRoster roster, CancellationToken cancellationToken = default);
}
