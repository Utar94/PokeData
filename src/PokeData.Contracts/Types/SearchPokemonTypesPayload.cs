using PokeData.Contracts.Search;

namespace PokeData.Contracts.Types;

public record SearchPokemonTypesPayload : SearchPayload
{
  public List<ushort> NumberIn { get; set; } = [];

  public new List<PokemonTypeSortOption> Sort { get; set; } = [];
}
