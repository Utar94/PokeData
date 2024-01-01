using Microsoft.EntityFrameworkCore;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational;

public class PokemonContext : DbContext
{
  public PokemonContext(DbContextOptions<PokemonContext> options) : base(options)
  {
  }

  internal DbSet<ActorEntity> Actors { get; private set; }
  internal DbSet<GenerationEntity> Generations { get; private set; }
  internal DbSet<PokemonRosterEntity> PokemonRoster { get; private set; }
  internal DbSet<PokemonSpeciesEntity> PokemonSpecies { get; private set; }
  internal DbSet<PokemonTypeEntity> PokemonTypes { get; private set; }
  internal DbSet<PokemonVarietyEntity> PokemonVarieties { get; private set; }
  internal DbSet<RegionEntity> Regions { get; private set; }
  internal DbSet<ResourceEntity> Resources { get; private set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}
