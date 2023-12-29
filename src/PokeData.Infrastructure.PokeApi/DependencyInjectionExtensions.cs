using Microsoft.Extensions.DependencyInjection;
using PokeData.Application.Resources;

namespace PokeData.Infrastructure.PokeApi;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataInfrastructurePokeApi(this IServiceCollection services)
  {
    return services
      .AddHttpClient()
      .AddPokeDataInfrastructure()
      .AddTransient<IResourceExtractor, ResourceExtractor>();
  }
}
