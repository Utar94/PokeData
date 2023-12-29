using Logitar.EventSourcing.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeData.EntityFrameworkCore.Relational;

namespace PokeData.EntityFrameworkCore.PostgreSQL;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataWithEntityFrameworkCorePostgreSQL(this IServiceCollection services, IConfiguration configuration)
  {
    string connectionString = configuration.GetValue<string>("POSTGRESQLCONNSTR_Pokemon") ?? string.Empty;

    return services.AddPokeDataWithEntityFrameworkCorePostgreSQL(connectionString);
  }

  public static IServiceCollection AddPokeDataWithEntityFrameworkCorePostgreSQL(this IServiceCollection services, string connectionString)
  {
    return services
      .AddDbContext<PokemonContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("PokeData.EntityFrameworkCore.PostgreSQL")))
      .AddLogitarEventSourcingWithEntityFrameworkCorePostgreSQL(connectionString)
      .AddPokeDataWithEntityFrameworkCoreRelational()
      .AddSingleton<ISqlHelper, PostgresHelper>();
  }
}
