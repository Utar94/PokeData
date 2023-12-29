using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Microsoft.Extensions.DependencyInjection;
using PokeData.Application.Resources;
using PokeData.Domain.Resources;
using PokeData.EntityFrameworkCore.Relational.Queries;
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
      .AddRepositories();
  }

  private static IServiceCollection AddQueriers(this IServiceCollection services)
  {
    return services.AddTransient<IResourceQuerier, ResourceQuerier>();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services.AddTransient<IResourceRepository, ResourceRepository>();
  }
}
