using System.Diagnostics;

namespace PokeData.Seeding;

internal class Worker : BackgroundService
{
  private readonly ILogger<Worker> _logger;

  public Worker(ILogger<Worker> logger)
  {
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    Stopwatch chrono = Stopwatch.StartNew();

    try
    {
      await Task.Delay(1000, cancellationToken); // TODO(fpion): implement
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, "An unhandled exception has occurred.");
    }

    chrono.Stop();
    _logger.LogInformation("Operation completed in {Elapsed}ms.", chrono.ElapsedMilliseconds);
  }
}
