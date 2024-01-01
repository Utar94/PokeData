using MediatR;

namespace PokeData.Application.Species.Commands;

public record ImportSpeciesCommand(IEnumerable<string> Ids) : INotification;
