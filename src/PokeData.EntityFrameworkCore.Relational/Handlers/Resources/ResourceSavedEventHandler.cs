using MediatR;
using Microsoft.EntityFrameworkCore;
using PokeData.Domain.Resources.Events;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Handlers.Resources;

internal class ResourceSavedEventHandler : INotificationHandler<ResourceSavedEvent>
{
  private readonly PokemonContext _context;

  public ResourceSavedEventHandler(PokemonContext context)
  {
    _context = context;
  }

  public async Task Handle(ResourceSavedEvent @event, CancellationToken cancellationToken)
  {
    ResourceEntity? entity = await _context.Resources
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (entity == null)
    {
      entity = new(@event);
      _context.Resources.Add(entity);
    }
    else
    {
      entity.Save(@event);
    }

    await _context.SaveChangesAsync(cancellationToken);
  }
}
