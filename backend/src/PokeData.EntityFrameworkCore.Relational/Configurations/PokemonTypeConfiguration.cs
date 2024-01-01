using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class PokemonTypeConfiguration : AggregateConfiguration<PokemonTypeEntity>, IEntityTypeConfiguration<PokemonTypeEntity>
{
  public override void Configure(EntityTypeBuilder<PokemonTypeEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(PokemonContext.PokemonTypes));
    builder.HasKey(x => x.PokemonTypeId);

    builder.HasIndex(x => x.UniqueName);
    builder.HasIndex(x => x.UniqueNameNormalized).IsUnique();
    builder.HasIndex(x => x.DisplayName);

    builder.Property(x => x.PokemonTypeId).ValueGeneratedNever();
    builder.Property(x => x.UniqueName).HasMaxLength(128);
    builder.Property(x => x.UniqueNameNormalized).HasMaxLength(128);
    builder.Property(x => x.DisplayName).HasMaxLength(128);
  }
}
