﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PokeData.EntityFrameworkCore.Relational;

#nullable disable

namespace PokeData.EntityFrameworkCore.PostgreSQL.Migrations
{
    [DbContext(typeof(PokemonContext))]
    partial class PokemonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PokeData.EntityFrameworkCore.Relational.Entities.ResourceEntity", b =>
                {
                    b.Property<long>("ResourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ResourceId"));

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("SourceNormalized")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("ResourceId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

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
#pragma warning restore 612, 618
        }
    }
}
