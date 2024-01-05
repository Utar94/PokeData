namespace PokeData.Domain.Roster;

public interface IPokemonRosterRepository
{
  Task DeleteAsync(ushort speciesId, CancellationToken cancellationToken = default);
  Task SaveAsync(PokemonRoster roster, CancellationToken cancellationToken = default);
}
