namespace PokeData.Contracts.Search;

public record SearchPayload
{
  public TextSearch Search { get; set; } = new();

  public List<SortOption> Sort { get; set; } = [];

  public int Skip { get; set; }
  public int Limit { get; set; }
}
