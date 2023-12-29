using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class TypeConfiguration : IEntityTypeConfiguration<TypeEntity>
{
  public void Configure(EntityTypeBuilder<TypeEntity> builder)
  {
    builder.ToTable(nameof(PokemonContext.Types));
    builder.HasKey(x => x.TypeId);

    builder.HasIndex(x => x.UniqueName).IsUnique();
    builder.HasIndex(x => x.DisplayName);

    builder.Property(x => x.UniqueName).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.DisplayName).HasMaxLength(byte.MaxValue);
  }
}
