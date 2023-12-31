using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class RegionConfiguration : AggregateConfiguration<RegionEntity>, IEntityTypeConfiguration<RegionEntity>
{
  public override void Configure(EntityTypeBuilder<RegionEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(PokemonContext.Regions));
    builder.HasKey(x => x.RegionId);

    builder.HasIndex(x => x.UniqueName);
    builder.HasIndex(x => x.UniqueNameNormalized).IsUnique();
    builder.HasIndex(x => x.DisplayName);

    builder.Property(x => x.RegionId).ValueGeneratedNever();
    builder.Property(x => x.UniqueName).HasMaxLength(128);
    builder.Property(x => x.UniqueNameNormalized).HasMaxLength(128);
    builder.Property(x => x.DisplayName).HasMaxLength(128);
  }
}
