namespace PokeData.Domain.Species;

public interface IPokemonSpeciesRepository
{
  Task<PokemonSpecies?> LoadAsync(ushort id, CancellationToken cancellationToken = default);
  Task<PokemonSpecies?> LoadAsync(string name, CancellationToken cancellationToken = default);
}
