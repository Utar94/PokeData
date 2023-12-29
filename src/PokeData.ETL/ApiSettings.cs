namespace PokeData.ETL;

internal record ApiSettings
{
  public string? BaseUrl { get; set; }
  public BasicCredentials? Basic { get; set; }
}

internal record BasicCredentials
{
  public string? Username { get; set; }
  public string? Password { get; set; }
}
