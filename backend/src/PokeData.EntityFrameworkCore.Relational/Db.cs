using Logitar.Data;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational;

internal static class Db
{
  public static class PokemonTypes
  {
    public static readonly TableId Table = new(nameof(PokemonContext.PokemonTypes));

    public static readonly ColumnId DisplayName = new(nameof(PokemonTypeEntity.DisplayName), Table);
    public static readonly ColumnId PokemonTypeId = new(nameof(PokemonTypeEntity.PokemonTypeId), Table);
    public static readonly ColumnId UniqueName = new(nameof(PokemonTypeEntity.UniqueName), Table);
    public static readonly ColumnId UniqueNameNormalized = new(nameof(PokemonTypeEntity.UniqueNameNormalized), Table);
  }

  public static class Regions
  {
    public static readonly TableId Table = new(nameof(PokemonContext.Regions));

    public static readonly ColumnId DisplayName = new(nameof(RegionEntity.DisplayName), Table);
    public static readonly ColumnId RegionId = new(nameof(RegionEntity.RegionId), Table);
    public static readonly ColumnId UniqueName = new(nameof(RegionEntity.UniqueName), Table);
    public static readonly ColumnId UniqueNameNormalized = new(nameof(RegionEntity.UniqueNameNormalized), Table);
  }
}
