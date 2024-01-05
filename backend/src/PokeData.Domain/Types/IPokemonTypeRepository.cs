namespace PokeData.Domain.Types;

public interface IPokemonTypeRepository
{
  Task<PokemonType?> LoadAsync(string idOrUniqueName, CancellationToken cancellationToken = default);
}
