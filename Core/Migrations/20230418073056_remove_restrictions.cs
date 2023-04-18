using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE.Migrations
{
    /// <inheritdoc />
    public partial class remove_restrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreenings_Restrictions_RestrictionId",
                table: "MovieScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Restrictions_RestrictionId",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropIndex(
                name: "IX_Seats_RestrictionId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_MovieScreenings_RestrictionId",
                table: "MovieScreenings");

            migrationBuilder.DropColumn(
                name: "RestrictionId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "RestrictionId",
                table: "MovieScreenings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestrictionId",
                table: "Seats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestrictionId",
                table: "MovieScreenings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    RestrictionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.RestrictionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RestrictionId",
                table: "Seats",
                column: "RestrictionId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreenings_RestrictionId",
                table: "MovieScreenings",
                column: "RestrictionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreenings_Restrictions_RestrictionId",
                table: "MovieScreenings",
                column: "RestrictionId",
                principalTable: "Restrictions",
                principalColumn: "RestrictionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Restrictions_RestrictionId",
                table: "Seats",
                column: "RestrictionId",
                principalTable: "Restrictions",
                principalColumn: "RestrictionId");
        }
    }
}
