using PokeData.Application.Resources;

namespace PokeData.Tools.Synchronization;

internal class FakeResourceExtractor : IResourceExtractor
{
  public Task<IEnumerable<Resource>> GetSpeciesAsync(string id, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
