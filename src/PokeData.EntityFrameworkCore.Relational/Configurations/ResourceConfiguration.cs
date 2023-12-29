using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class ResourceConfiguration : AggregateConfiguration<ResourceEntity>, IEntityTypeConfiguration<ResourceEntity>
{
  public override void Configure(EntityTypeBuilder<ResourceEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(PokemonContext.Resources));
    builder.HasKey(x => x.ResourceId);

    builder.HasIndex(x => x.Source);
    builder.HasIndex(x => x.SourceNormalized).IsUnique();

    builder.Property(x => x.Source).HasMaxLength(2048);
    builder.Property(x => x.SourceNormalized).HasMaxLength(2048);
  }
}
