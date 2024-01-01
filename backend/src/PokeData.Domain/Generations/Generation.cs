using PokeData.Domain.Regions;

namespace PokeData.Domain.Generations;

public class Generation
{
  public byte Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  public RegionAggregate? MainRegion { get; set; }
}
