namespace PokeData.ETL.Entities;

internal class PokemonType
{
  public int Number { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }

  public static PokemonType FromModel(Models.Type model, string languageName) => new()
  {
    Number = model.Id,
    UniqueName = model.Name,
    DisplayName = model.Names.SingleOrDefault(name => name.Language?.Name == languageName)?.Value
  };
}
