using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class PokemonSpeciesConfiguration : AggregateConfiguration<PokemonSpeciesEntity>, IEntityTypeConfiguration<PokemonSpeciesEntity>
{
  public override void Configure(EntityTypeBuilder<PokemonSpeciesEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(PokemonContext.PokemonSpecies));
    builder.HasKey(x => x.PokemonSpeciesId);

    builder.HasIndex(x => x.Order);
    builder.HasIndex(x => x.IsBaby);
    builder.HasIndex(x => x.IsLegendary);
    builder.HasIndex(x => x.IsMythical);
    builder.HasIndex(x => x.HasGenderDifferences);
    builder.HasIndex(x => x.CanSwitchForm);
    builder.HasIndex(x => x.UniqueName);
    builder.HasIndex(x => x.UniqueNameNormalized).IsUnique();
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.Category);

    builder.Property(x => x.PokemonSpeciesId).ValueGeneratedNever();
    builder.Property(x => x.UniqueName).HasMaxLength(128);
    builder.Property(x => x.UniqueNameNormalized).HasMaxLength(128);
    builder.Property(x => x.DisplayName).HasMaxLength(128);
    builder.Property(x => x.Category).HasMaxLength(128);

    builder.HasOne(x => x.Generation).WithMany(x => x.Species)
      .HasPrincipalKey(x => x.GenerationId).HasForeignKey(x => x.GenerationId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
