using Microsoft.Extensions.DependencyInjection;

namespace PokeData.Application;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataApplication(this IServiceCollection services)
  {
    return services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
  }
}
