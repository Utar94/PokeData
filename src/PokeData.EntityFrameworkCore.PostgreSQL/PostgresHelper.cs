using Logitar.Data;
using Logitar.Data.PostgreSQL;
using PokeData.EntityFrameworkCore.Relational;

namespace PokeData.EntityFrameworkCore.PostgreSQL;

internal class PostgresHelper : ISqlHelper
{
  public IQueryBuilder QueryFrom(TableId table) => PostgresQueryBuilder.From(table);
}
