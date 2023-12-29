namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class RegionEntity // TODO(fpion): private access
{
  public int RegionId { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
}
