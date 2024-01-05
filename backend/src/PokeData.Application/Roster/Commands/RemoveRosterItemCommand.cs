using MediatR;

namespace PokeData.Application.Roster.Commands;

internal record RemoveRosterItemCommand(ushort SpeciesId) : IRequest<Unit>;
