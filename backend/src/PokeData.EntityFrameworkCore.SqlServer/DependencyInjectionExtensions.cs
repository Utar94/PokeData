using Logitar.EventSourcing.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeData.EntityFrameworkCore.Relational;

namespace PokeData.EntityFrameworkCore.SqlServer;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddPokeDataWithEntityFrameworkCoreSqlServer(this IServiceCollection services, IConfiguration configuration)
  {
    string connectionString = configuration.GetValue<string>("SQLCONNSTR_Pokemon") ?? string.Empty;

    return services.AddPokeDataWithEntityFrameworkCoreSqlServer(connectionString);
  }
  public static IServiceCollection AddPokeDataWithEntityFrameworkCoreSqlServer(this IServiceCollection services, string connectionString)
  {
    return services
      .AddDbContext<PokemonContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("PokeData.EntityFrameworkCore.SqlServer")))
      .AddLogitarEventSourcingWithEntityFrameworkCoreSqlServer(connectionString)
      .AddPokeDataWithEntityFrameworkCoreRelational()
      .AddTransient<ISqlHelper, SqlServerHelper>();
  }
}
