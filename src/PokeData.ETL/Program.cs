namespace PokeData.ETL;

public class Program
{
  public static void Main(string[] args)
  {
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHostedService<Worker>();
    builder.Services.AddHttpClient();

    IHost host = builder.Build();
    host.Run();
  }
}
