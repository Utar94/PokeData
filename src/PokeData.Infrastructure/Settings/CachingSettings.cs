namespace PokeData.Infrastructure.Settings;

internal record CachingSettings
{
  public int ActorLifetimeSeconds { get; set; }
  public bool LoadResourcesOnStartup { get; set; }
  public int ResourceLifetimeSeconds { get; set; }
}
