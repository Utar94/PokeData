using Logitar.Data;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational;

internal static class PokemonDb
{
  public static class PokemonRoster
  {
    public static readonly TableId Table = new(nameof(PokemonContext.PokemonRoster));

    public static readonly ColumnId AggregateId = new(nameof(PokemonRosterEntity.AggregateId), Table);
    public static readonly ColumnId Category = new(nameof(PokemonRosterEntity.Category), Table);
    public static readonly ColumnId CreatedBy = new(nameof(PokemonRosterEntity.CreatedBy), Table);
    public static readonly ColumnId CreatedOn = new(nameof(PokemonRosterEntity.CreatedOn), Table);
    public static readonly ColumnId IsBaby = new(nameof(PokemonRosterEntity.IsBaby), Table);
    public static readonly ColumnId IsLegendary = new(nameof(PokemonRosterEntity.IsLegendary), Table);
    public static readonly ColumnId IsMythical = new(nameof(PokemonRosterEntity.IsMythical), Table);
    public static readonly ColumnId Name = new(nameof(PokemonRosterEntity.Name), Table);
    public static readonly ColumnId Number = new(nameof(PokemonRosterEntity.Number), Table);
    public static readonly ColumnId PokemonSpeciesId = new(nameof(PokemonRosterEntity.PokemonSpeciesId), Table);
    public static readonly ColumnId PrimaryTypeId = new(nameof(PokemonRosterEntity.PrimaryTypeId), Table);
    public static readonly ColumnId RegionId = new(nameof(PokemonRosterEntity.RegionId), Table);
    public static readonly ColumnId SecondaryTypeId = new(nameof(PokemonRosterEntity.SecondaryTypeId), Table);
    public static readonly ColumnId UpdatedBy = new(nameof(PokemonRosterEntity.UpdatedBy), Table);
    public static readonly ColumnId UpdatedOn = new(nameof(PokemonRosterEntity.UpdatedOn), Table);
    public static readonly ColumnId Version = new(nameof(PokemonRosterEntity.Version), Table);
  }

  public static class PokemonTypes
  {
    public static readonly TableId Table = new(nameof(PokemonContext.PokemonTypes));

    public static readonly ColumnId AggregateId = new(nameof(PokemonTypeEntity.AggregateId), Table);
    public static readonly ColumnId CreatedBy = new(nameof(PokemonTypeEntity.CreatedBy), Table);
    public static readonly ColumnId CreatedOn = new(nameof(PokemonTypeEntity.CreatedOn), Table);
    public static readonly ColumnId DisplayName = new(nameof(PokemonTypeEntity.DisplayName), Table);
    public static readonly ColumnId PokemonTypeId = new(nameof(PokemonTypeEntity.PokemonTypeId), Table);
    public static readonly ColumnId UniqueName = new(nameof(PokemonTypeEntity.UniqueName), Table);
    public static readonly ColumnId UniqueNameNormalized = new(nameof(PokemonTypeEntity.UniqueNameNormalized), Table);
    public static readonly ColumnId UpdatedBy = new(nameof(PokemonTypeEntity.UpdatedBy), Table);
    public static readonly ColumnId UpdatedOn = new(nameof(PokemonTypeEntity.UpdatedOn), Table);
    public static readonly ColumnId Version = new(nameof(PokemonTypeEntity.Version), Table);
  }

  public static class Regions
  {
    public static readonly TableId Table = new(nameof(PokemonContext.Regions));

    public static readonly ColumnId AggregateId = new(nameof(RegionEntity.AggregateId), Table);
    public static readonly ColumnId CreatedBy = new(nameof(RegionEntity.CreatedBy), Table);
    public static readonly ColumnId CreatedOn = new(nameof(RegionEntity.CreatedOn), Table);
    public static readonly ColumnId DisplayName = new(nameof(RegionEntity.DisplayName), Table);
    public static readonly ColumnId MainGenerationId = new(nameof(RegionEntity.MainGenerationId), Table);
    public static readonly ColumnId RegionId = new(nameof(RegionEntity.RegionId), Table);
    public static readonly ColumnId UniqueName = new(nameof(RegionEntity.UniqueName), Table);
    public static readonly ColumnId UniqueNameNormalized = new(nameof(RegionEntity.UniqueNameNormalized), Table);
    public static readonly ColumnId UpdatedBy = new(nameof(RegionEntity.UpdatedBy), Table);
    public static readonly ColumnId UpdatedOn = new(nameof(RegionEntity.UpdatedOn), Table);
    public static readonly ColumnId Version = new(nameof(RegionEntity.Version), Table);
  }

  public static string Normalize(string value) => value.Trim().ToUpper();
}
