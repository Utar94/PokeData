namespace PokeData.Contracts.Regions;

public record SaveRegionPayload
{
  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
}
