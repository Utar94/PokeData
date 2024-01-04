using PokeData.Contracts.Search;

namespace PokeData.Contracts.Types;

public record PokemonTypeSortOption : SortOption
{
  public new PokemonTypeSort Field
  {
    get => Enum.Parse<PokemonTypeSort>(base.Field);
    set => base.Field = value.ToString();
  }

  public PokemonTypeSortOption() : this(PokemonTypeSort.UpdatedOn, isDescending: true)
  {
  }
  public PokemonTypeSortOption(PokemonTypeSort field, bool isDescending = false) : base(field.ToString(), isDescending)
  {
  }
}
