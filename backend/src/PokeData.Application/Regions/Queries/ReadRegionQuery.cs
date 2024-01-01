using MediatR;
using PokeData.Contracts.Regions;

namespace PokeData.Application.Regions.Queries;

internal record ReadRegionQuery(byte? Number, string? UniqueName) : IRequest<Region?>;
