using PokeData.Application;
using PokeData.Application.Resources;
using PokeData.EntityFrameworkCore.PostgreSQL;

namespace PokeData.Tools.Synchronization;

public class Program
{
  public static void Main(string[] args)
  {
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHostedService<Worker>();

    // TODO(fpion): BEGIN REFACTOR
    builder.Services.AddPokeDataWithEntityFrameworkCorePostgreSQL(builder.Configuration);
    builder.Services.AddSingleton<IApplicationContext, SynchronizationApplicationContext>();
    builder.Services.AddSingleton<IResourceExtractor, FakeResourceExtractor>();
    // TODO(fpion): END REFACTOR

    IHost host = builder.Build();
    host.Run();
  }
}
