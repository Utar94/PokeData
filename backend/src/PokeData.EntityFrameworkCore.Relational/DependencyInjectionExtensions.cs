using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Microsoft.Extensions.DependencyInjection;
using PokeData.Application.Regions;
using PokeData.Application.Roster;
using PokeData.Application.Types;
using PokeData.Domain.Regions;
using PokeData.Domain.Resources;
using PokeData.Domain.Roster;
using PokeData.Domain.Species;
using PokeData.Domain.Types;
using PokeData.EntityFrameworkCore.Relational.Actors;
using PokeData.EntityFrameworkCore.Relational.Queriers;
using PokeData.EntityFrameworkCore.Relational.Repositories;
using PokeData.Infrastructure;

namespace PokeData.EntityFrameworkCore.Relational;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataWithEntityFrameworkCoreRelational(this IServiceCollection services)
  {
    return services
      .AddLogitarEventSourcingWithEntityFrameworkCoreRelational()
      .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
      .AddPokeDataInfrastructure()
      .AddQueriers()
      .AddRepositories()
      .AddTransient<IActorService, ActorService>();
  }

  private static IServiceCollection AddQueriers(this IServiceCollection services)
  {
    return services
      .AddTransient<IPokemonRosterQuerier, PokemonRosterQuerier>()
      .AddTransient<IPokemonTypeQuerier, PokemonTypeQuerier>()
      .AddTransient<IRegionQuerier, RegionQuerier>();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services
      .AddTransient<IPokemonRosterRepository, PokemonRosterRepository>()
      .AddTransient<IPokemonSpeciesRepository, PokemonSpeciesRepository>()
      .AddTransient<IPokemonTypeRepository, PokemonTypeRepository>()
      .AddTransient<IRegionRepository, RegionRepository>()
      .AddTransient<IResourceRepository, ResourceRepository>();
  }
}
