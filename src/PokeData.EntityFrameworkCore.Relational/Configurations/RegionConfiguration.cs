using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class RegionConfiguration : IEntityTypeConfiguration<RegionEntity>
{
  public void Configure(EntityTypeBuilder<RegionEntity> builder)
  {
    builder.ToTable(nameof(PokemonContext.Regions));
    builder.HasKey(x => x.RegionId);

    builder.HasIndex(x => x.UniqueName).IsUnique();
    builder.HasIndex(x => x.DisplayName);

    builder.Property(x => x.UniqueName).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.DisplayName).HasMaxLength(byte.MaxValue);
  }
}
