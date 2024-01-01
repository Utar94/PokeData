using MediatR;
using PokeData.Contracts.Regions;

namespace PokeData.Application.Regions.Commands;

internal record SaveRegionCommand(byte Number, SaveRegionPayload Payload) : IRequest<Region>;
