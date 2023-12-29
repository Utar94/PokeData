using Logitar.Data;
using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational;

internal static class Db
{
  public static class Events
  {
    public static readonly TableId Table = new(nameof(EventContext.Events));

    public static readonly ColumnId AggregateId = new(nameof(EventEntity.AggregateId), Table);
    public static readonly ColumnId AggregateType = new(nameof(EventEntity.AggregateType), Table);
  }

  public static class Resources
  {
    public static readonly TableId Table = new(nameof(PokemonContext.Resources));

    public static readonly ColumnId AggregateId = new(nameof(ResourceEntity.AggregateId), Table);
    public static readonly ColumnId SourceNormalized = new(nameof(ResourceEntity.SourceNormalized), Table);
  }
}
