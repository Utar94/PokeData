using Logitar.Data;
using PokeData.Contracts.Search;

namespace PokeData.EntityFrameworkCore.Relational;

public interface ISqlHelper
{
  IQueryBuilder ApplyTextSearch(IQueryBuilder builder, TextSearch search, params ColumnId[] columns);
  IQueryBuilder QueryFrom(TableId table);
}
