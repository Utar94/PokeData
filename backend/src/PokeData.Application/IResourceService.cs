using PokeData.Domain.Species;

namespace PokeData.Application;

public interface IResourceService
{
  Task<PokemonSpecies?> GetSpeciesAsync(string id, CancellationToken cancellationToken = default);
}
