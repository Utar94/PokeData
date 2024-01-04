using PokeData.Contracts.Search;

namespace PokeData.Contracts.Regions;

public record RegionSortOption : SortOption
{
  public new RegionSort Field
  {
    get => Enum.Parse<RegionSort>(base.Field);
    set => base.Field = value.ToString();
  }

  public RegionSortOption() : this(RegionSort.UpdatedOn, isDescending: true)
  {
  }
  public RegionSortOption(RegionSort field, bool isDescending = false) : base(field.ToString(), isDescending)
  {
  }
}
