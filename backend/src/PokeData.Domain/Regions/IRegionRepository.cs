namespace PokeData.Domain.Regions;

public interface IRegionRepository
{
  Task<RegionAggregate?> LoadAsync(byte id, CancellationToken cancellationToken = default);
  Task SaveAsync(RegionAggregate region, CancellationToken cancellationToken = default);
}
