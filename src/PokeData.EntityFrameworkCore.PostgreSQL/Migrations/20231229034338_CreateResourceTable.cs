using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokeData.EntityFrameworkCore.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class CreateResourceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Source = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    SourceNormalized = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Json = table.Column<string>(type: "text", nullable: false),
                    AggregateId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_AggregateId",
                table: "Resources",
                column: "AggregateId",
                unique: true);

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
                name: "Resources");
        }
    }
}
