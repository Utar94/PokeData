using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class GenerationConfiguration : IEntityTypeConfiguration<GenerationEntity>
{
  public void Configure(EntityTypeBuilder<GenerationEntity> builder)
  {
    builder.ToTable(nameof(PokemonContext.Generations));
    builder.HasKey(x => x.GenerationId);

    builder.HasIndex(x => x.UniqueName).IsUnique();
    builder.HasIndex(x => x.DisplayName);

    builder.Property(x => x.UniqueName).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.DisplayName).HasMaxLength(byte.MaxValue);
  }
}
