using PokeData.Contracts.Search;

namespace PokeData.Contracts.Types;

public interface IPokemonTypeService
{
  Task<PokemonType?> ReadAsync(byte? number, string? uniqueName, CancellationToken cancellationToken = default);
  Task<SearchResults<PokemonType>> SearchAsync(SearchPokemonTypesPayload payload, CancellationToken cancellationToken = default);
}
