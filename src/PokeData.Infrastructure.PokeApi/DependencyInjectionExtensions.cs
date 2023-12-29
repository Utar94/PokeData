using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeData.Application.Resources;

namespace PokeData.Infrastructure.PokeApi;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataInfrastructurePokeApi(this IServiceCollection services, IConfiguration configuration)
  {
    ResourceExtractorSettings settings = configuration.GetSection("ResourceExtractor").Get<ResourceExtractorSettings>() ?? new();

    return services.AddPokeDataInfrastructurePokeApi(settings);
  }

  public static IServiceCollection AddPokeDataInfrastructurePokeApi(this IServiceCollection services, ResourceExtractorSettings settings)
  {
    return services
      .AddHttpClient()
      .AddPokeDataInfrastructure()
      .AddSingleton(settings)
      .AddTransient<IResourceExtractor, ResourceExtractor>();
  }
}
