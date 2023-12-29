namespace PokeData.Infrastructure;

internal static class HttpResponseMessageExtensions
{
  public static async Task<HttpResponseDetail> DetailAsync(this HttpResponseMessage response, CancellationToken cancellationToken)
  {
    string? content = null;
    try
    {
      content = await response.Content.ReadAsStringAsync(cancellationToken);
    }
    catch (Exception)
    {
    }

    HttpResponseDetail detail = new()
    {
      Content = content,
      ReasonPhrase = response.ReasonPhrase,
      StatusCode = (int)response.StatusCode,
      StatusText = response.StatusCode.ToString(),
      Version = response.Version.ToString()
    };

    if (response.RequestMessage != null)
    {
      detail.RequestMethod = response.RequestMessage.ToString();
      detail.RequestUri = response.RequestMessage.RequestUri?.ToString();
    }

    return detail;
  }
}
