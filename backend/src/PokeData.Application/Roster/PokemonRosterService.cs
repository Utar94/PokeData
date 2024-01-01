using MediatR;
using PokeData.Application.Roster.Commands;
using PokeData.Application.Roster.Queries;
using PokeData.Contracts.Roster;

namespace PokeData.Application.Roster;

internal class PokemonRosterService : IPokemonRosterService
{
  private readonly IMediator _mediator;

  public PokemonRosterService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<PokemonRoster> GetAsync(CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadPokemonRosterQuery(), cancellationToken);
  }

  public async Task SaveItemAsync(ushort speciesId, SaveRosterItemPayload payload, CancellationToken cancellationToken)
  {
    await _mediator.Send(new SaveRosterItemCommand(speciesId, payload), cancellationToken);
  }
}
