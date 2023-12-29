using MediatR;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources;
using PokeData.Domain.Resources.Events;
using PokeData.EntityFrameworkCore.Relational.Entities;
using System.Text.Json;

namespace PokeData.EntityFrameworkCore.Relational.Handlers.Synchronization;

internal class TypeSynchronizationEventHandler : INotificationHandler<ResourceSavedEvent>
{

  private readonly PokemonContext _context;

  public TypeSynchronizationEventHandler(PokemonContext context)
  {
    _context = context;
  }

  public async Task Handle(ResourceSavedEvent @event, CancellationToken cancellationToken)
  {
    ResourceId resourceId = new(@event.AggregateId);
    if (resourceId.Type == typeof(Models.Type).Name)
    {
      Models.Type? type = JsonSerializer.Deserialize<Models.Type>(@event.Json.Value);
      if (type != null)
      {
        TypeEntity? entity = await _context.Types
          .SingleOrDefaultAsync(x => x.TypeId == type.Id, cancellationToken);
        if (entity == null)
        {
          entity = new()
          {
            TypeId = type.Id
          };
          _context.Types.Add(entity);
        }

        entity.UniqueName = type.UniqueName;
        entity.DisplayName = type.DisplayNames.SingleOrDefault(name => name.Language?.Name == "en")?.Value; // TODO(fpion): configuration

        await _context.SaveChangesAsync(cancellationToken);
      }
    }
  }
}
