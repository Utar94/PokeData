namespace PokeData.Domain.Types;

public interface IPokemonTypeRepository
{
  Task<PokemonType?> LoadAsync(byte id, CancellationToken cancellationToken = default);
}
