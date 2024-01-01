using MediatR;
using PokeData.Contracts.Regions;

namespace PokeData.Application.Regions.Queries;

internal class ReadRegionQueryHandler : IRequestHandler<ReadRegionQuery, Region?>
{
  private readonly IRegionQuerier _regionQuerier;

  public ReadRegionQueryHandler(IRegionQuerier regionQuerier)
  {
    _regionQuerier = regionQuerier;
  }

  public async Task<Region?> Handle(ReadRegionQuery query, CancellationToken cancellationToken)
  {
    Dictionary<ushort, Region> results = new(capacity: 2);

    if (query.Number.HasValue)
    {
      Region? region = await _regionQuerier.ReadAsync(query.Number.Value, cancellationToken);
      if (region != null)
      {
        results[region.Number] = region;
      }
    }
    if (!string.IsNullOrWhiteSpace(query.UniqueName))
    {
      Region? region = await _regionQuerier.ReadAsync(query.UniqueName, cancellationToken);
      if (region != null)
      {
        results[region.Number] = region;
      }
    }

    if (results.Count > 1)
    {
      throw new TooManyResultsException<Region>(expectedCount: 1, actualCount: results.Count);
    }

    return results.Values.SingleOrDefault();
  }
}
