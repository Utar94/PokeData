using MediatR;

namespace PokeData.Application.Resources.Commands;

public record ImportSpeciesResourcesCommand(string Id) : IRequest<Unit>;
