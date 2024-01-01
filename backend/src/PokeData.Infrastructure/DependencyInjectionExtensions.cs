using Logitar.EventSourcing.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeData.Application;
using PokeData.Application.Caching;
using PokeData.Infrastructure.Caching;
using PokeData.Infrastructure.Settings;

namespace PokeData.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataInfrastructure(this IServiceCollection services)
  {
    return services
      .AddLogitarEventSourcingInfrastructure()
      .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
      .AddPokeDataApplication()
      .AddSingleton(serviceProvider =>
      {
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetSection("Caching").Get<CachingSettings>() ?? new();
      })
      .AddSingleton<ICacheService, CacheService>()
      .AddTransient<IEventBus, EventBus>();
  }
}
