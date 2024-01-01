using MediatR;
using PokeData.Contracts.Roster;

namespace PokeData.Application.Roster.Queries;

internal class ReadPokemonRosterQueryHandler : IRequestHandler<ReadPokemonRosterQuery, PokemonRoster>
{
  private readonly IPokemonRosterQuerier _rosterQuerier;

  public ReadPokemonRosterQueryHandler(IPokemonRosterQuerier rosterQuerier)
  {
    _rosterQuerier = rosterQuerier;
  }

  public async Task<PokemonRoster> Handle(ReadPokemonRosterQuery _, CancellationToken cancellationToken)
  {
    return await _rosterQuerier.ReadAsync(cancellationToken);
  }
}
