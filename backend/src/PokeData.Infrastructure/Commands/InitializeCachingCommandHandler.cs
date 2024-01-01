using MediatR;
using PokeData.Application.Caching;
using PokeData.Domain.Resources;
using PokeData.Infrastructure.Settings;

namespace PokeData.Infrastructure.Commands;

internal class InitializeCachingCommandHandler : INotificationHandler<InitializeCachingCommand>
{
  private readonly ICacheService _cacheService;
  private readonly CachingSettings _cachingSettings;
  private readonly IResourceRepository _resourceRepository;

  public InitializeCachingCommandHandler(ICacheService cacheService, CachingSettings cachingSettings, IResourceRepository resourceRepository)
  {
    _cacheService = cacheService;
    _cachingSettings = cachingSettings;
    _resourceRepository = resourceRepository;
  }

  public async Task Handle(InitializeCachingCommand _, CancellationToken cancellationToken)
  {
    if (_cachingSettings.LoadResourcesOnStartup)
    {
      IEnumerable<Resource> resources = await _resourceRepository.GetAsync(cancellationToken);
      foreach (Resource resource in resources)
      {
        _cacheService.SetResource(resource);
      }
    }
  }
}
