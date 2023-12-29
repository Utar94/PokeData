using Logitar.Data;

namespace PokeData.EntityFrameworkCore.Relational;

public interface ISqlHelper
{
  IQueryBuilder QueryFrom(TableId table);
}
