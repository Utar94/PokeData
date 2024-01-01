using MediatR;
using PokeData.Contracts.Roster;

namespace PokeData.Application.Roster.Queries;

internal record ReadPokemonRosterQuery : IRequest<PokemonRoster>;
