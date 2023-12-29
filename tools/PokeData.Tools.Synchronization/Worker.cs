using Logitar;
using Logitar.EventSourcing;
using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.EventSourcing.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources;
using PokeData.Infrastructure.Commands;
using System.Diagnostics;

namespace PokeData.Tools.Synchronization;

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

    using IServiceScope scope = _serviceProvider.CreateScope();
    IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
    using EventContext context = scope.ServiceProvider.GetRequiredService<EventContext>();
    IEventSerializer serializer = scope.ServiceProvider.GetRequiredService<IEventSerializer>();
    IEventBus bus = scope.ServiceProvider.GetRequiredService<IEventBus>();

    await publisher.Publish(new InitializeDatabaseCommand(), cancellationToken);

    string aggregateType = typeof(ResourceAggregate).GetNamespaceQualifiedName();
    EventEntity[] events = await context.Events.AsNoTracking()
      .Where(e => e.AggregateType == aggregateType)
      .OrderBy(e => e.OccurredOn)
      .ToArrayAsync(cancellationToken);
    _logger.LogInformation("Found {count} events.", events.Length);

    for (int i = 0; i < events.Length; i++)
    {
      double percentage = (i + 1) / (double)events.Length;
      _logger.LogInformation("Handling event {index} of {total} ({percentage:P2}).", i + 1, events.Length, percentage);

      DomainEvent @event = serializer.Deserialize(events[i]);
      await bus.PublishAsync(@event, cancellationToken);
    }

    chrono.Stop();
    _logger.LogInformation("Operation completed in {elapsed} milliseconds.", chrono.ElapsedMilliseconds);
  }
}
