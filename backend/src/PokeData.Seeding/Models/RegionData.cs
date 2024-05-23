using CsvHelper.Configuration.Attributes;

namespace PokeData.Seeding.Models;

public record RegionData
{
  [Index(0)]
  public byte Number { get; set; }

  [Index(1)]
  public string UniqueName { get; set; } = string.Empty;

  [Index(2)]
  public string? DisplayName { get; set; }
}
