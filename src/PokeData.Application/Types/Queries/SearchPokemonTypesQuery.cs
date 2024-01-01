using MediatR;
using PokeData.Contracts.Search;
using PokeData.Contracts.Types;

namespace PokeData.Application.Types.Queries;

internal record SearchPokemonTypesQuery(SearchPokemonTypesPayload Payload) : IRequest<SearchResults<PokemonType>>;
