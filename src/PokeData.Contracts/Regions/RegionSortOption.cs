using PokeData.Contracts.Search;

namespace PokeData.Contracts.Regions;

public record RegionSortOption : SortOption
{
  public new RegionSort Field => Enum.Parse<RegionSort>(base.Field);

  public RegionSortOption() : this(RegionSort.UpdatedOn, isDescending: true)
  {
  }
  public RegionSortOption(RegionSort field, bool isDescending = false) : base(field.ToString(), isDescending)
  {
  }
}
