namespace PokeData.Contracts.Roster;

public interface IPokemonRosterService
{
  Task<PokemonRoster> GetAsync(CancellationToken cancellationToken = default);
  Task SaveItemAsync(ushort speciesId, SaveRosterItemPayload payload, CancellationToken cancellationToken = default);
}
