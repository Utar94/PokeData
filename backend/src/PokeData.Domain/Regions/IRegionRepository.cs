namespace PokeData.Domain.Regions;

public interface IRegionRepository
{
  Task<RegionAggregate?> LoadAsync(string idOrUniqueName, CancellationToken cancellationToken = default);
  Task SaveAsync(RegionAggregate region, CancellationToken cancellationToken = default);
}
