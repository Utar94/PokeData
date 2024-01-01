using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeData.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    PokemonTypeId = table.Column<int>(type: "int", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UniqueNameNormalized = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.PokemonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UniqueNameNormalized = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MainGenerationId = table.Column<int>(type: "int", nullable: true),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    SourceNormalized = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                });

            migrationBuilder.CreateTable(
                name: "Generations",
                columns: table => new
                {
                    GenerationId = table.Column<int>(type: "int", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UniqueNameNormalized = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MainRegionId = table.Column<int>(type: "int", nullable: false),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generations", x => x.GenerationId);
                    table.ForeignKey(
                        name: "FK_Generations_Regions_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSpecies",
                columns: table => new
                {
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    GenerationId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsBaby = table.Column<bool>(type: "bit", nullable: false),
                    IsLegendary = table.Column<bool>(type: "bit", nullable: false),
                    IsMythical = table.Column<bool>(type: "bit", nullable: false),
                    HasGenderDifferences = table.Column<bool>(type: "bit", nullable: false),
                    CanSwitchForm = table.Column<bool>(type: "bit", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UniqueNameNormalized = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    GenderRatio = table.Column<double>(type: "float", nullable: true),
                    CatchRate = table.Column<byte>(type: "tinyint", nullable: false),
                    HatchTime = table.Column<byte>(type: "tinyint", nullable: false),
                    BaseFriendship = table.Column<byte>(type: "tinyint", nullable: false),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSpecies", x => x.PokemonSpeciesId);
                    table.ForeignKey(
                        name: "FK_PokemonSpecies_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Generations",
                        principalColumn: "GenerationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonVarieties",
                columns: table => new
                {
                    PokemonVarietyId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UniqueNameNormalized = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PrimaryTypeId = table.Column<int>(type: "int", nullable: false),
                    SecondaryTypeId = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    BaseExperienceYield = table.Column<int>(type: "int", nullable: false),
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVarieties", x => x.PokemonVarietyId);
                    table.ForeignKey(
                        name: "FK_PokemonVarieties_PokemonSpecies_PokemonSpeciesId",
                        column: x => x.PokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "PokemonSpeciesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonVarieties_PokemonTypes_PrimaryTypeId",
                        column: x => x.PrimaryTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonVarieties_PokemonTypes_SecondaryTypeId",
                        column: x => x.SecondaryTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Generations_AggregateId",
                table: "Generations",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Generations_CreatedBy",
                table: "Generations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_CreatedOn",
                table: "Generations",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_DisplayName",
                table: "Generations",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_UniqueName",
                table: "Generations",
                column: "UniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_UniqueNameNormalized",
                table: "Generations",
                column: "UniqueNameNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Generations_UpdatedBy",
                table: "Generations",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_UpdatedOn",
                table: "Generations",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_Version",
                table: "Generations",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_AggregateId",
                table: "PokemonSpecies",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_CanSwitchForm",
                table: "PokemonSpecies",
                column: "CanSwitchForm");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_Category",
                table: "PokemonSpecies",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_CreatedBy",
                table: "PokemonSpecies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_CreatedOn",
                table: "PokemonSpecies",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_DisplayName",
                table: "PokemonSpecies",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_GenerationId",
                table: "PokemonSpecies",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_HasGenderDifferences",
                table: "PokemonSpecies",
                column: "HasGenderDifferences");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_IsBaby",
                table: "PokemonSpecies",
                column: "IsBaby");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_IsLegendary",
                table: "PokemonSpecies",
                column: "IsLegendary");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_IsMythical",
                table: "PokemonSpecies",
                column: "IsMythical");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_Order",
                table: "PokemonSpecies",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_UniqueName",
                table: "PokemonSpecies",
                column: "UniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_UniqueNameNormalized",
                table: "PokemonSpecies",
                column: "UniqueNameNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_UpdatedBy",
                table: "PokemonSpecies",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_UpdatedOn",
                table: "PokemonSpecies",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_Version",
                table: "PokemonSpecies",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_AggregateId",
                table: "PokemonTypes",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_CreatedBy",
                table: "PokemonTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_CreatedOn",
                table: "PokemonTypes",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_DisplayName",
                table: "PokemonTypes",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_UniqueName",
                table: "PokemonTypes",
                column: "UniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_UniqueNameNormalized",
                table: "PokemonTypes",
                column: "UniqueNameNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_UpdatedBy",
                table: "PokemonTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_UpdatedOn",
                table: "PokemonTypes",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_Version",
                table: "PokemonTypes",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_AggregateId",
                table: "PokemonVarieties",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_CreatedBy",
                table: "PokemonVarieties",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_CreatedOn",
                table: "PokemonVarieties",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_IsDefault",
                table: "PokemonVarieties",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_Order",
                table: "PokemonVarieties",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_PokemonSpeciesId",
                table: "PokemonVarieties",
                column: "PokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_PrimaryTypeId",
                table: "PokemonVarieties",
                column: "PrimaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_SecondaryTypeId",
                table: "PokemonVarieties",
                column: "SecondaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_UniqueName",
                table: "PokemonVarieties",
                column: "UniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_UniqueNameNormalized",
                table: "PokemonVarieties",
                column: "UniqueNameNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_UpdatedBy",
                table: "PokemonVarieties",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_UpdatedOn",
                table: "PokemonVarieties",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarieties_Version",
                table: "PokemonVarieties",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_AggregateId",
                table: "Regions",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CreatedBy",
                table: "Regions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CreatedOn",
                table: "Regions",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_DisplayName",
                table: "Regions",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_UniqueName",
                table: "Regions",
                column: "UniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_UniqueNameNormalized",
                table: "Regions",
                column: "UniqueNameNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_UpdatedBy",
                table: "Regions",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_UpdatedOn",
                table: "Regions",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Version",
                table: "Regions",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_AggregateId",
                table: "Resources",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ContentType",
                table: "Resources",
                column: "ContentType");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CreatedBy",
                table: "Resources",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CreatedOn",
                table: "Resources",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Source",
                table: "Resources",
                column: "Source");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_SourceNormalized",
                table: "Resources",
                column: "SourceNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UpdatedBy",
                table: "Resources",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UpdatedOn",
                table: "Resources",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Version",
                table: "Resources",
                column: "Version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonVarieties");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "PokemonSpecies");

            migrationBuilder.DropTable(
                name: "PokemonTypes");

            migrationBuilder.DropTable(
                name: "Generations");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
