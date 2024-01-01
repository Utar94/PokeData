using PokeData.Contracts.Search;
using PokeData.Contracts.Types;

namespace PokeData.Application.Types;

public interface IPokemonTypeQuerier
{
  Task<PokemonType?> ReadAsync(byte number, CancellationToken cancellationToken = default);
  Task<PokemonType?> ReadAsync(string uniqueName, CancellationToken cancellationToken = default);
  Task<SearchResults<PokemonType>> SearchAsync(SearchPokemonTypesPayload payload, CancellationToken cancellationToken = default);
}
