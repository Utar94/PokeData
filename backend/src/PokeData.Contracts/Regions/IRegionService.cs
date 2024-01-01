using PokeData.Contracts.Search;

namespace PokeData.Contracts.Regions;

public interface IRegionService
{
  Task<Region?> ReadAsync(byte? number, string? uniqueName, CancellationToken cancellationToken = default);
  Task<Region> SaveAsync(byte number, SaveRegionPayload payload, CancellationToken cancellationToken = default);
  Task<SearchResults<Region>> SearchAsync(SearchRegionsPayload payload, CancellationToken cancellationToken = default);
}
