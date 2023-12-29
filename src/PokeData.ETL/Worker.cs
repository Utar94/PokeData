using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace PokeData.ETL;

public class Worker : BackgroundService
{
  private readonly ApiSettings _apiSettings;
  private readonly ImportSettings _importSettings;
  private readonly WarmupSettings _warmupSettings;

  private readonly ILogger<Worker> _logger;

  public Worker(IConfiguration configuration, ILogger<Worker> logger)
  {
    _apiSettings = configuration.GetSection("Api").Get<ApiSettings>() ?? new();
    _importSettings = configuration.GetSection("Import").Get<ImportSettings>() ?? new();
    _warmupSettings = configuration.GetSection("Warmup").Get<WarmupSettings>() ?? new();

    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    await WarmupAsync(cancellationToken);

    Stopwatch chrono = Stopwatch.StartNew();

    using HttpClient client = new();
    if (_apiSettings.BaseUrl != null)
    {
      client.BaseAddress = new Uri(_apiSettings.BaseUrl, UriKind.Absolute);
    }
    if (_apiSettings.Basic != null && _apiSettings.Basic.Username != null && _apiSettings.Basic.Password != null)
    {
      string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(':', _apiSettings.Basic.Username, _apiSettings.Basic.Password)));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
    }

    for (int number = _importSettings.MinimumIndex; number <= _importSettings.MaximumIndex; number++)
    {
      Uri requestUri = new($"/species/import/{number}", UriKind.Relative);
      using HttpRequestMessage request = new(HttpMethod.Post, requestUri);
      using HttpResponseMessage response = await client.SendAsync(request, cancellationToken);
      response.EnsureSuccessStatusCode();

      _logger.LogInformation("Imported Pokémon species #{number}.", number.ToString("0000"));
    }

    chrono.Stop();
    _logger.LogInformation("Operation completed in {ms}ms.", chrono.ElapsedMilliseconds);
  }

  private async Task WarmupAsync(CancellationToken cancellationToken)
  {
    for (int seconds = _warmupSettings.SecondsDelay; seconds > 0; seconds--)
    {
      _logger.LogInformation("Worker starting in {seconds} second{plural}...", seconds, seconds > 1 ? 's' : string.Empty);
      await Task.Delay(1000, cancellationToken);
    }
  }
}
