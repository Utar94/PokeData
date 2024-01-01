using Logitar.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokeData.Contracts.Actors;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Configurations;

internal class ActorConfiguration : IEntityTypeConfiguration<ActorEntity>
{
  public void Configure(EntityTypeBuilder<ActorEntity> builder)
  {
    builder.ToTable(nameof(PokemonContext.Actors));
    builder.HasKey(x => x.ActorId);

    builder.HasIndex(x => x.Id).IsUnique();
    builder.HasIndex(x => x.Type);
    builder.HasIndex(x => x.IsDeleted);
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.EmailAddress);

    builder.Property(x => x.Id).IsRequired().HasMaxLength(ActorId.MaximumLength);
    builder.Property(x => x.Type).HasMaxLength(byte.MaxValue).HasConversion<EnumToStringConverter<ActorType>>();
    builder.Property(x => x.DisplayName).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.EmailAddress).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.PictureUrl).HasMaxLength(2048);
  }
}
