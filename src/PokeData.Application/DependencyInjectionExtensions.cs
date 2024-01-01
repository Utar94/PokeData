using Microsoft.Extensions.DependencyInjection;
using PokeData.Application.Regions;
using PokeData.Application.Roster;
using PokeData.Application.Types;
using PokeData.Contracts.Regions;
using PokeData.Contracts.Roster;
using PokeData.Contracts.Types;

namespace PokeData.Application;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataApplication(this IServiceCollection services)
  {
    return services
      .AddApplicationServices()
      .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
  }

  private static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    return services
      .AddTransient<IPokemonRosterService, PokemonRosterService>()
      .AddTransient<IPokemonTypeService, PokemonTypeService>()
      .AddTransient<IRegionService, RegionService>();
  }
}
