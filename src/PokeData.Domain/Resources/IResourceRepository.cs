namespace PokeData.Domain.Resources;

public interface IResourceRepository
{
  Task<ResourceAggregate?> LoadAsync<T>(string identifier, CancellationToken cancellationToken = default);
  Task<ResourceAggregate?> LoadAsync(ResourceId id, CancellationToken cancellationToken = default);
  Task<ResourceAggregate?> LoadAsync(SourceUnit source, CancellationToken cancellationToken = default);

  Task<IEnumerable<ResourceAggregate>> LoadAsync(IEnumerable<ResourceId> ids, CancellationToken cancellationToken = default);

  Task SaveAsync(ResourceAggregate resource, CancellationToken cancellationToken = default);
  Task SaveAsync(IEnumerable<ResourceAggregate> resources, CancellationToken cancellationToken = default);
}
