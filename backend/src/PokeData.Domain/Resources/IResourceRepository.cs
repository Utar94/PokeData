namespace PokeData.Domain.Resources;

public interface IResourceRepository
{
  Task<IEnumerable<Resource>> GetAsync(CancellationToken cancellationToken = default);
  Task<Resource?> GetAsync(Uri source, CancellationToken cancellationToken = default);
  Task SaveAsync(Resource resource, CancellationToken cancellationToken = default);
}
