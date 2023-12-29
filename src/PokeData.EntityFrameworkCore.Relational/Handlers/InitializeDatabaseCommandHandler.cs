using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PokeData.Infrastructure.Commands;

namespace PokeData.EntityFrameworkCore.Relational.Handlers;

internal class InitializeDatabaseCommandHandler : INotificationHandler<InitializeDatabaseCommand>
{
  private readonly IConfiguration _configuration;
  private readonly EventContext _eventContext;
  private readonly PokemonContext _pokemonContext;

  public InitializeDatabaseCommandHandler(IConfiguration configuration, EventContext eventContext, PokemonContext pokemonContext)
  {
    _configuration = configuration;
    _eventContext = eventContext;
    _pokemonContext = pokemonContext;
  }

  public async Task Handle(InitializeDatabaseCommand _, CancellationToken cancellationToken)
  {
    if (_configuration.GetValue<bool>("EnableMigrations"))
    {
      await _eventContext.Database.MigrateAsync(cancellationToken);
      await _pokemonContext.Database.MigrateAsync(cancellationToken);
    }
  }
}
