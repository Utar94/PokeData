using MediatR;
using PokeData.Contracts.Roster;

namespace PokeData.Application.Roster.Commands;

internal record SaveRosterItemCommand(ushort SpeciesId, SaveRosterItemPayload Payload) : IRequest<Unit>;
