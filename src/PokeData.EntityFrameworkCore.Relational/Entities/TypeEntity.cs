namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class TypeEntity // TODO(fpion): private access
{
  public int TypeId { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
}
