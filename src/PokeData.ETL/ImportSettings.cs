namespace PokeData.ETL;

internal record ImportSettings
{
  public int MinimumIndex { get; set; } = 1;
  public int MaximumIndex { get; set; } = 1025;
}
