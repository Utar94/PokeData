using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class PokemonRosterConfiguration : AggregateConfiguration<PokemonRosterEntity>, IEntityTypeConfiguration<PokemonRosterEntity>
{
  public override void Configure(EntityTypeBuilder<PokemonRosterEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(PokemonContext.PokemonRoster));
    builder.HasKey(x => x.PokemonSpeciesId);

    builder.HasIndex(x => x.Number);
    builder.HasIndex(x => x.Name);
    builder.HasIndex(x => x.Category);
    builder.HasIndex(x => x.IsBaby);
    builder.HasIndex(x => x.IsLegendary);
    builder.HasIndex(x => x.IsMythical);

    builder.Property(x => x.Name).HasMaxLength(128);
    builder.Property(x => x.Category).HasMaxLength(128);

    builder.HasOne(x => x.PokemonSpecies).WithOne(x => x.Roster)
      .HasPrincipalKey<PokemonSpeciesEntity>(x => x.PokemonSpeciesId).HasForeignKey<PokemonRosterEntity>(x => x.PokemonSpeciesId)
      .OnDelete(DeleteBehavior.Cascade);
    builder.HasOne(x => x.Region).WithMany(x => x.Roster).OnDelete(DeleteBehavior.Restrict);
  }
}
