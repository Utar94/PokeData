using Logitar.EventSourcing;
using PokeData.Domain;

namespace PokeData.EntityFrameworkCore.Relational.Entities;

internal class PokemonTypeEntity : AggregateEntity
{
  public int PokemonTypeId { get; private set; }

  public string UniqueName { get; private set; } = string.Empty;
  public string UniqueNameNormalized
  {
    get => PokemonDb.Normalize(UniqueName);
    private set { }
  }
  public string? DisplayName { get; private set; }

  public PokemonTypeEntity(PokemonType pokemonType)
  {
    AggregateId = Logitar.EventSourcing.AggregateId.NewId().ToString();
    Version = 1;
    CreatedBy = UpdatedBy = ActorId.DefaultValue;
    CreatedOn = UpdatedOn = DateTime.UtcNow;

    PokemonTypeId = pokemonType.Number;

    UniqueName = pokemonType.UniqueName;
    DisplayName = pokemonType.DisplayName;
  }

  private PokemonTypeEntity()
  {
  }

  public void Update(PokemonType pokemonType)
  {
    if (pokemonType.UniqueName != UniqueName || pokemonType.DisplayName != DisplayName)
    {
      Version++;
      UpdatedBy = ActorId.DefaultValue;
      UpdatedOn = DateTime.UtcNow;

      UniqueName = pokemonType.UniqueName;
      DisplayName = pokemonType.DisplayName;
    }
  }
}
