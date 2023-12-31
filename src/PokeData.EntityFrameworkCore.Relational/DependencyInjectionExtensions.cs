using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Microsoft.Extensions.DependencyInjection;
using PokeData.Domain.Resources;
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
      .AddRepositories();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services.AddTransient<IResourceRepository, ResourceRepository>();
  }
}
