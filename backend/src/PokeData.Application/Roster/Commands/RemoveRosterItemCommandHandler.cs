using MediatR;
using PokeData.Domain.Roster;

namespace PokeData.Application.Roster.Commands;

internal class RemoveRosterItemCommandHandler : IRequestHandler<RemoveRosterItemCommand, Unit>
{
  private readonly IPokemonRosterRepository _rosterRepository;

  public RemoveRosterItemCommandHandler(IPokemonRosterRepository rosterRepository)
  {
    _rosterRepository = rosterRepository;
  }

  public async Task<Unit> Handle(RemoveRosterItemCommand command, CancellationToken cancellationToken)
  {
    await _rosterRepository.DeleteAsync(command.SpeciesId, cancellationToken);
    return Unit.Value;
  }
}
