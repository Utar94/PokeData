using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class PokemonVarietyConfiguration : AggregateConfiguration<PokemonVarietyEntity>, IEntityTypeConfiguration<PokemonVarietyEntity>
{
  public override void Configure(EntityTypeBuilder<PokemonVarietyEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(PokemonContext.PokemonVarieties));
    builder.HasKey(x => x.PokemonVarietyId);

    builder.HasIndex(x => x.Order);
    builder.HasIndex(x => x.UniqueName);
    builder.HasIndex(x => x.UniqueNameNormalized).IsUnique();
    builder.HasIndex(x => x.IsDefault);

    builder.Property(x => x.PokemonVarietyId).ValueGeneratedNever();
    builder.Property(x => x.UniqueName).HasMaxLength(128);
    builder.Property(x => x.UniqueNameNormalized).HasMaxLength(128);

    builder.HasOne(x => x.Species).WithMany(x => x.Varieties)
      .HasPrincipalKey(x => x.PokemonSpeciesId).HasForeignKey(x => x.PokemonSpeciesId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
