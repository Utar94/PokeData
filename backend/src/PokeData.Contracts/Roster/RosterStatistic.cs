namespace PokeData.Contracts.Roster;

public record RosterStatistic
{
  public string Key { get; set; } = string.Empty;
  public int Count { get; set; }
  public double Percentage { get; set; }

  public static RosterStatistic Create(string key, int count, int total) => new()
  {
    Key = key,
    Count = count,
    Percentage = total == 0 ? 0 : count / (double)total
  };
}
