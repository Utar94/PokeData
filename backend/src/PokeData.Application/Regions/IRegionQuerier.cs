using PokeData.Contracts.Regions;
using PokeData.Contracts.Search;

namespace PokeData.Application.Regions;

public interface IRegionQuerier
{
  Task<Region?> ReadAsync(byte number, CancellationToken cancellationToken = default);
  Task<Region?> ReadAsync(string uniqueName, CancellationToken cancellationToken = default);
  Task<SearchResults<Region>> SearchAsync(SearchRegionsPayload payload, CancellationToken cancellationToken = default);
}
