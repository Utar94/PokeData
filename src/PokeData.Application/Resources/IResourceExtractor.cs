namespace PokeData.Application.Resources;

public interface IResourceExtractor
{
  Task<IEnumerable<Resource>> GetSpeciesAsync(string id, CancellationToken cancellationToken = default);
}
