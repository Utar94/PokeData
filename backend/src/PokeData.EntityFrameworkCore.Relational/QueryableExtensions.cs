using Logitar.Data;
using Microsoft.EntityFrameworkCore;
using PokeData.Contracts.Search;

namespace PokeData.EntityFrameworkCore.Relational;

internal static class QueryableExtensions
{
  public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, SearchPayload payload)
  {
    if (payload.Skip > 0)
    {
      query = query.Skip(payload.Skip);
    }
    if (payload.Limit > 0)
    {
      query = query.Take(payload.Limit);
    }

    return query;
  }

  public static IQueryable<T> FromQuery<T>(this DbSet<T> set, IQueryBuilder builder) where T : class
  {
    return set.FromQuery(builder.Build());
  }
  public static IQueryable<T> FromQuery<T>(this DbSet<T> set, IQuery query) where T : class
  {
    return set.FromSqlRaw(query.Text, query.Parameters.ToArray());
  }
}
