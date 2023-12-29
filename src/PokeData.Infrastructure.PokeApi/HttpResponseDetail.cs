namespace PokeData.Infrastructure;

public record HttpResponseDetail
{
  public string? Content { get; set; }
  public string? ReasonPhrase { get; set; }
  public string? RequestMethod { get; set; }
  public string? RequestUri { get; set; }
  public int StatusCode { get; set; }
  public string StatusText { get; set; } = string.Empty;
  public string Version { get; set; } = string.Empty;
}
