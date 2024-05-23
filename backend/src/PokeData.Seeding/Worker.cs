using MediatR;
using PokeData.Seeding.Commands;

namespace PokeData.Seeding;

internal class Worker : BackgroundService
{
  private readonly Encoding _encoding;
  private readonly ILogger<Worker> _logger;
  private readonly IServiceProvider _serviceProvider;

  public Worker(IConfiguration configuration, ILogger<Worker> logger, IServiceProvider serviceProvider)
  {
    _encoding = Encoding.GetEncoding(configuration.GetValue<string>("Encoding") ?? string.Empty);
    _logger = logger;
    _serviceProvider = serviceProvider;
  }

  protected override async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    Stopwatch chrono = Stopwatch.StartNew();

    try
    {
      using IServiceScope scope = _serviceProvider.CreateScope();
      IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

      await publisher.Publish(new SeedRegionsCommand("Data/Regions.csv", _encoding), cancellationToken);
      await publisher.Publish(new SeedRosterCommand("Data/Roster", _encoding), cancellationToken);
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, "An unhandled exception has occurred.");
    }

    chrono.Stop();
    _logger.LogInformation("Operation completed in {Elapsed}ms.", chrono.ElapsedMilliseconds);
  }
}
