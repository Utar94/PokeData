using MediatR;
using PokeData.Application.Regions.Commands;
using PokeData.Application.Regions.Queries;
using PokeData.Contracts.Regions;
using PokeData.Contracts.Search;

namespace PokeData.Application.Regions;

internal class RegionService : IRegionService
{
  private readonly IMediator _mediator;

  public RegionService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<Region?> ReadAsync(byte? number, string? uniqueName, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadRegionQuery(number, uniqueName), cancellationToken);
  }

  public async Task<Region> SaveAsync(byte number, SaveRegionPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SaveRegionCommand(number, payload), cancellationToken);
  }

  public async Task<SearchResults<Region>> SearchAsync(SearchRegionsPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchRegionsQuery(payload), cancellationToken);
  }
}
