using Logitar.EventSourcing.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PokeData.Application;
using PokeData.Application.Caching;
using PokeData.Infrastructure.Caching;

namespace PokeData.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataInfrastructure(this IServiceCollection services)
  {
    return services
      .AddLogitarEventSourcingInfrastructure()
      .AddMemoryCache()
      .AddPokeDataApplication()
      .AddSingleton<ICacheService, CacheService>()
      .AddTransient<IEventBus, EventBus>();
  }
}
