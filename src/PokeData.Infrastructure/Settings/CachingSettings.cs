namespace PokeData.Infrastructure.Settings;

internal record CachingSettings
{
  public bool LoadResourcesOnStartup { get; set; }
  public int ResourceLifetimeSeconds { get; set; }
}
