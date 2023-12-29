using Logitar.Data;
using Logitar.Data.SqlServer;
using PokeData.EntityFrameworkCore.Relational;

namespace PokeData.EntityFrameworkCore.SqlServer;

internal class SqlServerHelper : ISqlHelper
{
  public IQueryBuilder QueryFrom(TableId table) => SqlServerQueryBuilder.From(table);
}
