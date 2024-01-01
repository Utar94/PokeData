using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeData.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class CreateActorAndPokemonRosterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "PokemonRoster",
                columns: table => new
                {
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    PrimaryTypeId = table.Column<int>(type: "int", nullable: false),
                    SecondaryTypeId = table.Column<int>(type: "int", nullable: true),
                    IsBaby = table.Column<bool>(type: "bit", nullable: false),
                    IsLegendary = table.Column<bool>(type: "bit", nullable: false),
                    IsMythical = table.Column<bool>(type: "bit", nullable: false),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonRoster", x => x.PokemonSpeciesId);
                    table.ForeignKey(
                        name: "FK_PokemonRoster_PokemonSpecies_PokemonSpeciesId",
                        column: x => x.PokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "PokemonSpeciesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonRoster_PokemonTypes_PrimaryTypeId",
                        column: x => x.PrimaryTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonRoster_PokemonTypes_SecondaryTypeId",
                        column: x => x.SecondaryTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId");
                    table.ForeignKey(
                        name: "FK_PokemonRoster_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_DisplayName",
                table: "Actors",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_EmailAddress",
                table: "Actors",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Id",
                table: "Actors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_IsDeleted",
                table: "Actors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Type",
                table: "Actors",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_AggregateId",
                table: "PokemonRoster",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_Category",
                table: "PokemonRoster",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_CreatedBy",
                table: "PokemonRoster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_CreatedOn",
                table: "PokemonRoster",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_IsBaby",
                table: "PokemonRoster",
                column: "IsBaby");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_IsLegendary",
                table: "PokemonRoster",
                column: "IsLegendary");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_IsMythical",
                table: "PokemonRoster",
                column: "IsMythical");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_Name",
                table: "PokemonRoster",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_Number",
                table: "PokemonRoster",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_PrimaryTypeId",
                table: "PokemonRoster",
                column: "PrimaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_RegionId",
                table: "PokemonRoster",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_SecondaryTypeId",
                table: "PokemonRoster",
                column: "SecondaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_UpdatedBy",
                table: "PokemonRoster",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_UpdatedOn",
                table: "PokemonRoster",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRoster_Version",
                table: "PokemonRoster",
                column: "Version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "PokemonRoster");
        }
    }
}
