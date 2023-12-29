using PokeData.Application;
using PokeData.EntityFrameworkCore.PostgreSQL;
using PokeData.EntityFrameworkCore.SqlServer;
using PokeData.Infrastructure.PokeApi;

namespace PokeData;

internal class Startup : StartupBase
{
  private readonly IConfiguration _configuration;
  private readonly bool _enableOpenApi;

  public Startup(IConfiguration configuration)
  {
    _configuration = configuration;
    _enableOpenApi = configuration.GetValue<bool>("EnableOpenApi");
  }

  public override void ConfigureServices(IServiceCollection services)
  {
    base.ConfigureServices(services);

    services.AddControllers();

    if (_enableOpenApi)
    {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
    }

    services.AddPokeDataInfrastructurePokeApi();
    services.AddSingleton<IApplicationContext, HttpApplicationContext>();

    DatabaseProvider databaseProvider = _configuration.GetValue<DatabaseProvider?>("DatabaseProvider")
      ?? DatabaseProvider.EntityFrameworkCorePostgreSQL;
    switch (databaseProvider)
    {
      case DatabaseProvider.EntityFrameworkCorePostgreSQL:
        services.AddPokeDataWithEntityFrameworkCorePostgreSQL(_configuration);
        break;
      case DatabaseProvider.EntityFrameworkCoreSqlServer:
        services.AddPokeDataWithEntityFrameworkCoreSqlServer(_configuration);
        break;
      default:
        throw new DatabaseProviderNotSupportedException(databaseProvider);
    }
  }

  public override void Configure(IApplicationBuilder builder)
  {
    if (_enableOpenApi)
    {
      builder.UseSwagger();
      builder.UseSwaggerUI();
    }

    builder.UseHttpsRedirection();
    builder.UseAuthentication();
    builder.UseAuthorization();

    if (builder is WebApplication application)
    {
      application.MapControllers();
    }
  }
}
