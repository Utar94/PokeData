using MediatR;
using PokeData.Application.Species.Commands;
using PokeData.Infrastructure.Commands;
using System.Diagnostics;

namespace PokeData.ETL;

public class Worker : BackgroundService
{
  private readonly ILogger<Worker> _logger;
  private readonly IServiceProvider _serviceProvider;

  public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
  {
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
      await publisher.Publish(new InitializeDatabaseCommand(), cancellationToken);
      await publisher.Publish(new InitializeCachingCommand(), cancellationToken);

      IEnumerable<string> ids = Enumerable.Range(1, 1025).Select(id => id.ToString());
      await publisher.Publish(new ImportSpeciesCommand(ids), cancellationToken);
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, "An unhandled exception has occurred.");
    }

    chrono.Stop();
    _logger.LogInformation("Operation completed in {Elapsed}ms.", chrono.ElapsedMilliseconds);
  }
}
