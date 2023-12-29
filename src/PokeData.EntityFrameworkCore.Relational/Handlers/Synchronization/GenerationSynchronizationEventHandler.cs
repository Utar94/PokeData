using MediatR;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources;
using PokeData.Domain.Resources.Events;
using PokeData.EntityFrameworkCore.Relational.Entities;
using PokeData.EntityFrameworkCore.Relational.Models;
using System.Text.Json;

namespace PokeData.EntityFrameworkCore.Relational.Handlers.Synchronization;

internal class GenerationSynchronizationEventHandler : INotificationHandler<ResourceSavedEvent>
{

  private readonly PokemonContext _context;

  public GenerationSynchronizationEventHandler(PokemonContext context)
  {
    _context = context;
  }

  public async Task Handle(ResourceSavedEvent @event, CancellationToken cancellationToken)
  {
    ResourceId resourceId = new(@event.AggregateId);
    if (resourceId.Type == typeof(Generation).Name)
    {
      Generation? generation = JsonSerializer.Deserialize<Generation>(@event.Json.Value);
      if (generation != null)
      {
        GenerationEntity? entity = await _context.Generations
          .SingleOrDefaultAsync(x => x.GenerationId == generation.Id, cancellationToken);
        if (entity == null)
        {
          entity = new()
          {
            GenerationId = generation.Id
          };
          _context.Generations.Add(entity);
        }

        entity.UniqueName = generation.UniqueName;
        entity.DisplayName = generation.DisplayNames.SingleOrDefault(name => name.Language?.Name == "en")?.Value; // TODO(fpion): configuration

        await _context.SaveChangesAsync(cancellationToken);
      }
    }
  }
}
