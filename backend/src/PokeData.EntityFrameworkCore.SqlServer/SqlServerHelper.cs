using Logitar.Data;
using Logitar.Data.SqlServer;
using PokeData.EntityFrameworkCore.Relational;

namespace PokeData.EntityFrameworkCore.SqlServer;

internal class SqlServerHelper : SqlHelper
{
  public override IQueryBuilder QueryFrom(TableId table) => SqlServerQueryBuilder.From(table);
}
