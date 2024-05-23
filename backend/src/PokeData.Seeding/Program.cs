using PokeData.EntityFrameworkCore.SqlServer;
using PokeData.Infrastructure.PokeApiClient; // TODO(fpion): remove this dependency

namespace PokeData.Seeding;

internal class Program
{
  public static void Main(string[] args)
  {
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHostedService<Worker>();
    builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    builder.Services.AddPokeDataWithEntityFrameworkCoreSqlServer(builder.Configuration);
    builder.Services.AddPokeDataWithPokeApiClient();

    IHost host = builder.Build();
    host.Run();
  }
}
