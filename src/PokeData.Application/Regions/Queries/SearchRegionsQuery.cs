using MediatR;
using PokeData.Contracts.Regions;
using PokeData.Contracts.Search;

namespace PokeData.Application.Regions.Queries;

internal record SearchRegionsQuery(SearchRegionsPayload Payload) : IRequest<SearchResults<Region>>;
