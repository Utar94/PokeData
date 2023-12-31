using PokeData.EntityFrameworkCore.SqlServer;
using PokeData.Infrastructure.PokeApiClient;

namespace PokeData.ETL;

public class Program
{
  public static void Main(string[] args)
  {
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHostedService<Worker>();
    builder.Services.AddMemoryCache();
    builder.Services.AddPokeDataWithEntityFrameworkCoreSqlServer(builder.Configuration);
    builder.Services.AddPokeDataWithPokeApiClient();

    IHost host = builder.Build();
    host.Run();
  }
}
