using PokeData.Contracts.Search;

namespace PokeData.Contracts.Regions;

public record SearchRegionsPayload : SearchPayload
{
  public List<ushort> NumberIn { get; set; } = [];

  public new List<RegionSortOption> Sort { get; set; } = [];
}
