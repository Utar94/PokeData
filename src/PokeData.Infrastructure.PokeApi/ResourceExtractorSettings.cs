namespace PokeData.Infrastructure.PokeApi;

public record ResourceExtractorSettings
{
  public DelaySettings? Delay { get; set; }
}

public record DelaySettings
{
  public int Minimum { get; set; }
  public int Maximum { get; set; }
}
