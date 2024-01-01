using Microsoft.Extensions.DependencyInjection;
using PokeData.Application;

namespace PokeData.Infrastructure.PokeApiClient;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataWithPokeApiClient(this IServiceCollection services)
  {
    return services
      .AddHttpClient()
      .AddPokeDataInfrastructure()
      .AddTransient<IResourceService, ResourceService>();
  }
}
