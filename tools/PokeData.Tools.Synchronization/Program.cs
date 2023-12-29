using PokeData.Application;
using PokeData.EntityFrameworkCore.PostgreSQL;
using PokeData.Infrastructure.PokeApi;

namespace PokeData.Tools.Synchronization;

public class Program
{
  public static void Main(string[] args)
  {
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHostedService<Worker>();

    // TODO(fpion): BEGIN REFACTOR
    builder.Services.AddPokeDataInfrastructurePokeApi(builder.Configuration);
    builder.Services.AddPokeDataWithEntityFrameworkCorePostgreSQL(builder.Configuration);
    builder.Services.AddSingleton<IApplicationContext, SynchronizationApplicationContext>();
    // TODO(fpion): END REFACTOR

    IHost host = builder.Build();
    host.Run();
  }
}
