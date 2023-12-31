﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokeData.EntityFrameworkCore.Relational;

#nullable disable

namespace PokeData.EntityFrameworkCore.SqlServer.Migrations
{
    [DbContext(typeof(PokemonContext))]
    [Migration("20231231153029_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.GenerationEntity", b =>
                {
                    b.Property<int>("GenerationId")
                        .HasColumnType("int");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("MainRegionId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UniqueNameNormalized")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("GenerationId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

                    b.HasIndex("CreatedBy");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("DisplayName");

                    b.HasIndex("UniqueName");

                    b.HasIndex("UniqueNameNormalized")
                        .IsUnique();

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UpdatedOn");

                    b.HasIndex("Version");

                    b.ToTable("Generations", (string)null);
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.PokemonSpeciesEntity", b =>
                {
                    b.Property<int>("PokemonSpeciesId")
                        .HasColumnType("int");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte>("BaseFriendship")
                        .HasColumnType("tinyint");

                    b.Property<bool>("CanSwitchForm")
                        .HasColumnType("bit");

                    b.Property<byte>("CatchRate")
                        .HasColumnType("tinyint");

                    b.Property<string>("Category")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<double?>("GenderRatio")
                        .HasColumnType("float");

                    b.Property<int>("GenerationId")
                        .HasColumnType("int");

                    b.Property<bool>("HasGenderDifferences")
                        .HasColumnType("bit");

                    b.Property<byte>("HatchTime")
                        .HasColumnType("tinyint");

                    b.Property<bool>("IsBaby")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLegendary")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMythical")
                        .HasColumnType("bit");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UniqueNameNormalized")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("PokemonSpeciesId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

                    b.HasIndex("CanSwitchForm");

                    b.HasIndex("Category");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("DisplayName");

                    b.HasIndex("GenerationId");

                    b.HasIndex("HasGenderDifferences");

                    b.HasIndex("IsBaby");

                    b.HasIndex("IsLegendary");

                    b.HasIndex("IsMythical");

                    b.HasIndex("Order");

                    b.HasIndex("UniqueName");

                    b.HasIndex("UniqueNameNormalized")
                        .IsUnique();

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UpdatedOn");

                    b.HasIndex("Version");

                    b.ToTable("PokemonSpecies", (string)null);
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.PokemonTypeEntity", b =>
                {
                    b.Property<int>("PokemonTypeId")
                        .HasColumnType("int");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UniqueNameNormalized")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("PokemonTypeId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

                    b.HasIndex("CreatedBy");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("DisplayName");

                    b.HasIndex("UniqueName");

                    b.HasIndex("UniqueNameNormalized")
                        .IsUnique();

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UpdatedOn");

                    b.HasIndex("Version");

                    b.ToTable("PokemonTypes", (string)null);
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.PokemonVarietyEntity", b =>
                {
                    b.Property<int>("PokemonVarietyId")
                        .HasColumnType("int");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("BaseExperienceYield")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("PokemonSpeciesId")
                        .HasColumnType("int");

                    b.Property<int>("PrimaryTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("SecondaryTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UniqueNameNormalized")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("PokemonVarietyId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

                    b.HasIndex("CreatedBy");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("IsDefault");

                    b.HasIndex("Order");

                    b.HasIndex("PokemonSpeciesId");

                    b.HasIndex("PrimaryTypeId");

                    b.HasIndex("SecondaryTypeId");

                    b.HasIndex("UniqueName");

                    b.HasIndex("UniqueNameNormalized")
                        .IsUnique();

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UpdatedOn");

                    b.HasIndex("Version");

                    b.ToTable("PokemonVarieties", (string)null);
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.RegionEntity", b =>
                {
                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("MainGenerationId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UniqueNameNormalized")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("RegionId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

                    b.HasIndex("CreatedBy");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("DisplayName");

                    b.HasIndex("UniqueName");

                    b.HasIndex("UniqueNameNormalized")
                        .IsUnique();

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UpdatedOn");

                    b.HasIndex("Version");

                    b.ToTable("Regions", (string)null);
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.ResourceEntity", b =>
                {
                    b.Property<int>("ResourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResourceId"));

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("SourceNormalized")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("ResourceId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

                    b.HasIndex("ContentType");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("Source");

                    b.HasIndex("SourceNormalized")
                        .IsUnique();

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UpdatedOn");

                    b.HasIndex("Version");

                    b.ToTable("Resources", (string)null);
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.GenerationEntity", b =>
                {
                    b.HasOne("PokeData.EntityFrameworkCore.Relational.Entities.RegionEntity", "MainRegion")
                        .WithOne("MainGeneration")
                        .HasForeignKey("PokeData.EntityFrameworkCore.Relational.Entities.GenerationEntity", "GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainRegion");
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.PokemonSpeciesEntity", b =>
                {
                    b.HasOne("PokeData.EntityFrameworkCore.Relational.Entities.GenerationEntity", "Generation")
                        .WithMany("Species")
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Generation");
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.PokemonVarietyEntity", b =>
                {
                    b.HasOne("PokeData.EntityFrameworkCore.Relational.Entities.PokemonSpeciesEntity", "Species")
                        .WithMany("Varieties")
                        .HasForeignKey("PokemonSpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokeData.EntityFrameworkCore.Relational.Entities.PokemonTypeEntity", "PrimaryType")
                        .WithMany()
                        .HasForeignKey("PrimaryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokeData.EntityFrameworkCore.Relational.Entities.PokemonTypeEntity", "SecondaryType")
                        .WithMany()
                        .HasForeignKey("SecondaryTypeId");

                    b.Navigation("PrimaryType");

                    b.Navigation("SecondaryType");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.GenerationEntity", b =>
                {
                    b.Navigation("Species");
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.PokemonSpeciesEntity", b =>
                {
                    b.Navigation("Varieties");
                });

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.RegionEntity", b =>
                {
                    b.Navigation("MainGeneration");
                });
#pragma warning restore 612, 618
        }
    }
}
