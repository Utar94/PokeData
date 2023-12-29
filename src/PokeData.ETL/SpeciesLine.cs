using CsvHelper.Configuration.Attributes;

namespace PokeData.ETL;

internal record SpeciesLine
{
  [Index(0)]
  public string? Region { get; set; }

  [Index(1)]
  public string? Number { get; set; }

  [Index(2)]
  public string? Name { get; set; }

  [Index(3)]
  public string? PrimaryType { get; set; }

  [Index(4)]
  public string? SecondaryType { get; set; }
}
