using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class GenerationConfiguration : AggregateConfiguration<GenerationEntity>, IEntityTypeConfiguration<GenerationEntity>
{
  public override void Configure(EntityTypeBuilder<GenerationEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(PokemonContext.Generations));
    builder.HasKey(x => x.GenerationId);

    builder.HasIndex(x => x.UniqueName);
    builder.HasIndex(x => x.UniqueNameNormalized).IsUnique();
    builder.HasIndex(x => x.DisplayName);

    builder.Property(x => x.GenerationId).ValueGeneratedNever();
    builder.Property(x => x.UniqueName).HasMaxLength(128);
    builder.Property(x => x.UniqueNameNormalized).HasMaxLength(128);
    builder.Property(x => x.DisplayName).HasMaxLength(128);

    builder.HasOne(x => x.MainRegion).WithOne(x => x.MainGeneration)
      .HasPrincipalKey<RegionEntity>(x => x.RegionId).HasForeignKey<GenerationEntity>(x => x.GenerationId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
