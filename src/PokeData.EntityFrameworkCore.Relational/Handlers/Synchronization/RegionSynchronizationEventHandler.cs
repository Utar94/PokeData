using MediatR;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources;
using PokeData.Domain.Resources.Events;
using PokeData.EntityFrameworkCore.Relational.Entities;
using PokeData.EntityFrameworkCore.Relational.Models;
using System.Text.Json;

namespace PokeData.EntityFrameworkCore.Relational.Handlers.Synchronization;

internal class RegionSynchronizationEventHandler : INotificationHandler<ResourceSavedEvent>
{

  private readonly PokemonContext _context;

  public RegionSynchronizationEventHandler(PokemonContext context)
  {
    _context = context;
  }

  public async Task Handle(ResourceSavedEvent @event, CancellationToken cancellationToken)
  {
    ResourceId resourceId = new(@event.AggregateId);
    if (resourceId.Type == typeof(Region).Name)
    {
      Region? region = JsonSerializer.Deserialize<Region>(@event.Json.Value);
      if (region != null)
      {
        RegionEntity? entity = await _context.Regions
          .SingleOrDefaultAsync(x => x.RegionId == region.Id, cancellationToken);
        if (entity == null)
        {
          entity = new()
          {
            RegionId = region.Id
          };
          _context.Regions.Add(entity);
        }

        entity.UniqueName = region.UniqueName;
        entity.DisplayName = region.DisplayNames.SingleOrDefault(name => name.Language?.Name == "en")?.Value; // TODO(fpion): configuration

        await _context.SaveChangesAsync(cancellationToken);
      }
    }
  }
}
