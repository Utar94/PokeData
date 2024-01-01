using Logitar.Data;
using Logitar.EventSourcing;
using Microsoft.EntityFrameworkCore;
using PokeData.Application.Types;
using PokeData.Contracts.Actors;
using PokeData.Contracts.Search;
using PokeData.Contracts.Types;
using PokeData.EntityFrameworkCore.Relational.Actors;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Queriers;

internal class PokemonTypeQuerier : IPokemonTypeQuerier
{
  private readonly IActorService _actorService;
  private readonly DbSet<PokemonTypeEntity> _regions;
  private readonly ISqlHelper _sqlHelper;

  public PokemonTypeQuerier(IActorService actorService, PokemonContext context, ISqlHelper sqlHelper)
  {
    _actorService = actorService;
    _regions = context.PokemonTypes;
    _sqlHelper = sqlHelper;
  }

  public async Task<PokemonType?> ReadAsync(byte number, CancellationToken cancellationToken)
  {
    PokemonTypeEntity? region = await _regions.AsNoTracking()
      .SingleOrDefaultAsync(x => x.PokemonTypeId == number, cancellationToken);

    return region == null ? null : await MapAsync(region, cancellationToken);
  }

  public async Task<PokemonType?> ReadAsync(string uniqueName, CancellationToken cancellationToken)
  {
    string uniqueNameNormalized = uniqueName.Trim().ToUpper();

    PokemonTypeEntity? region = await _regions.AsNoTracking()
      .SingleOrDefaultAsync(x => x.UniqueNameNormalized == uniqueNameNormalized, cancellationToken);

    return region == null ? null : await MapAsync(region, cancellationToken);
  }

  public async Task<SearchResults<PokemonType>> SearchAsync(SearchPokemonTypesPayload payload, CancellationToken cancellationToken)
  {
    IQueryBuilder builder = _sqlHelper.QueryFrom(Db.PokemonTypes.Table)
      .SelectAll(Db.PokemonTypes.Table);
    _sqlHelper.ApplyTextSearch(builder, payload.Search, Db.PokemonTypes.UniqueName, Db.PokemonTypes.DisplayName);

    if (payload.NumberIn.Count > 1)
    {
      builder.Where(Db.PokemonTypes.PokemonTypeId, Operators.IsIn(payload.NumberIn));
    }

    IQueryable<PokemonTypeEntity> query = _regions.FromQuery(builder).AsNoTracking();
    long total = await query.LongCountAsync(cancellationToken);

    IOrderedQueryable<PokemonTypeEntity>? ordered = null;
    foreach (PokemonTypeSortOption sort in payload.Sort)
    {
      switch (sort.Field)
      {
        case PokemonTypeSort.DisplayName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.DisplayName) : ordered.ThenBy(x => x.DisplayName));
          break;
        case PokemonTypeSort.Number:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.PokemonTypeId) : query.OrderBy(x => x.PokemonTypeId))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.PokemonTypeId) : ordered.ThenBy(x => x.PokemonTypeId));
          break;
        case PokemonTypeSort.UniqueName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UniqueName) : query.OrderBy(x => x.UniqueName))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.UniqueName) : ordered.ThenBy(x => x.UniqueName));
          break;
        case PokemonTypeSort.UpdatedOn:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn) : query.OrderBy(x => x.UpdatedOn))
            : (sort.IsDescending ? ordered.ThenByDescending(x => x.UpdatedOn) : ordered.ThenBy(x => x.UpdatedOn));
          break;
      }
    }
    query = ordered ?? query;

    query = query.ApplyPaging(payload);

    PokemonTypeEntity[] regions = await query.ToArrayAsync(cancellationToken);
    IEnumerable<PokemonType> items = await MapAsync(regions, cancellationToken);

    return new SearchResults<PokemonType>(items, total);
  }

  private async Task<PokemonType> MapAsync(PokemonTypeEntity type, CancellationToken cancellationToken)
    => (await MapAsync([type], cancellationToken)).Single();
  private async Task<IEnumerable<PokemonType>> MapAsync(IEnumerable<PokemonTypeEntity> types, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> actorIds = types.SelectMany(region => region.GetActorIds());
    IEnumerable<Actor> actors = await _actorService.FindAsync(actorIds, cancellationToken);
    Mapper mapper = new(actors);

    return types.Select(mapper.ToPokemonType);
  }
}
