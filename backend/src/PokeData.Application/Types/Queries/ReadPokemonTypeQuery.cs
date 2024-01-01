using MediatR;
using PokeData.Contracts.Types;

namespace PokeData.Application.Types.Queries;

internal record ReadPokemonTypeQuery(byte? Number, string? UniqueName) : IRequest<PokemonType?>;
