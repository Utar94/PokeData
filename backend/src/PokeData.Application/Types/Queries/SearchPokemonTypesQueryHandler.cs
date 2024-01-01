using MediatR;
using PokeData.Contracts.Search;
using PokeData.Contracts.Types;

namespace PokeData.Application.Types.Queries;

internal class SearchPokemonTypesQueryHandler : IRequestHandler<SearchPokemonTypesQuery, SearchResults<PokemonType>>
{
  private readonly IPokemonTypeQuerier _regionQuerier;

  public SearchPokemonTypesQueryHandler(IPokemonTypeQuerier regionQuerier)
  {
    _regionQuerier = regionQuerier;
  }

  public async Task<SearchResults<PokemonType>> Handle(SearchPokemonTypesQuery query, CancellationToken cancellationToken)
  {
    return await _regionQuerier.SearchAsync(query.Payload, cancellationToken);
  }
}
