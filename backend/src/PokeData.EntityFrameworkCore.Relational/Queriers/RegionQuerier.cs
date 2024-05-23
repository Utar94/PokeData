using Logitar.Data;
using Logitar.EventSourcing;
using Microsoft.EntityFrameworkCore;
using PokeData.Application.Regions;
using PokeData.Contracts.Actors;
using PokeData.Contracts.Regions;
using PokeData.Contracts.Search;
using PokeData.EntityFrameworkCore.Relational.Actors;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Queriers;

internal class RegionQuerier : IRegionQuerier
{
  private readonly IActorService _actorService;
  private readonly DbSet<RegionEntity> _regions;
  private readonly ISqlHelper _sqlHelper;

  public RegionQuerier(IActorService actorService, PokemonContext context, ISqlHelper sqlHelper)
  {
    _actorService = actorService;
    _regions = context.Regions;
    _sqlHelper = sqlHelper;
  }

  public async Task<Region?> ReadAsync(byte number, CancellationToken cancellationToken)
  {
    RegionEntity? region = await _regions.AsNoTracking()
      .SingleOrDefaultAsync(x => x.RegionId == number, cancellationToken);

    return region == null ? null : await MapAsync(region, cancellationToken);
  }

  public async Task<Region?> ReadAsync(string uniqueName, CancellationToken cancellationToken)
  {
    string uniqueNameNormalized = PokemonDb.Normalize(uniqueName);

    RegionEntity? region = await _regions.AsNoTracking()
      .SingleOrDefaultAsync(x => x.UniqueNameNormalized == uniqueNameNormalized, cancellationToken);

    return region == null ? null : await MapAsync(region, cancellationToken);
  }

  public async Task<SearchResults<Region>> SearchAsync(SearchRegionsPayload payload, CancellationToken cancellationToken)
  {
    IQueryBuilder builder = _sqlHelper.QueryFrom(PokemonDb.Regions.Table)
      .SelectAll(PokemonDb.Regions.Table);
    _sqlHelper.ApplyTextSearch(builder, payload.Search, PokemonDb.Regions.UniqueName, PokemonDb.Regions.DisplayName);

    if (payload.NumberIn.Count > 1)
    {
      builder.Where(PokemonDb.Regions.RegionId, Operators.IsIn(payload.NumberIn));
    }

    IQueryable<RegionEntity> query = _regions.FromQuery(builder)
      .AsNoTracking()
      .Include(x => x.MainGeneration);
    long total = await query.LongCountAsync(cancellationToken);

    IOrderedQueryable<RegionEntity>? ordered = null;
    foreach (RegionSortOption sort in payload.Sort)
    {
      switch (sort.Field)
      {
        case RegionSort.DisplayName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.DisplayName) : ordered.ThenBy(x => x.DisplayName));
          break;
        case RegionSort.Number:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.RegionId) : query.OrderBy(x => x.RegionId))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.RegionId) : ordered.ThenBy(x => x.RegionId));
          break;
        case RegionSort.UniqueName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UniqueName) : query.OrderBy(x => x.UniqueName))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.UniqueName) : ordered.ThenBy(x => x.UniqueName));
          break;
        case RegionSort.UpdatedOn:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn) : query.OrderBy(x => x.UpdatedOn))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.UpdatedOn) : ordered.ThenBy(x => x.UpdatedOn));
          break;
      }
    }
    query = ordered ?? query;

    query = query.ApplyPaging(payload);

    RegionEntity[] regions = await query.ToArrayAsync(cancellationToken);
    IEnumerable<Region> items = await MapAsync(regions, cancellationToken);

    return new SearchResults<Region>(items, total);
  }

  private async Task<Region> MapAsync(RegionEntity region, CancellationToken cancellationToken)
    => (await MapAsync([region], cancellationToken)).Single();
  private async Task<IEnumerable<Region>> MapAsync(IEnumerable<RegionEntity> regions, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> actorIds = regions.SelectMany(region => region.GetActorIds());
    IEnumerable<Actor> actors = await _actorService.FindAsync(actorIds, cancellationToken);
    Mapper mapper = new(actors);

    return regions.Select(mapper.ToRegion);
  }
}
