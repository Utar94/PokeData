namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class GenerationEntity // TODO(fpion): private access
{
  public int GenerationId { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
}
