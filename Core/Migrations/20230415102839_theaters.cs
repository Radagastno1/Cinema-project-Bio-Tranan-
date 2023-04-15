using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE.Migrations
{
    /// <inheritdoc />
    public partial class theaters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreening_Theater_TheaterId",
                table: "MovieScreening");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Theater_TheaterId",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Theater",
                table: "Theater");

            migrationBuilder.RenameTable(
                name: "Theater",
                newName: "Theaters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Theaters",
                table: "Theaters",
                column: "TheaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreening_Theaters_TheaterId",
                table: "MovieScreening",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "TheaterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Theaters_TheaterId",
                table: "Seats",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "TheaterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreening_Theaters_TheaterId",
                table: "MovieScreening");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Theaters_TheaterId",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Theaters",
                table: "Theaters");

            migrationBuilder.RenameTable(
                name: "Theaters",
                newName: "Theater");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Theater",
                table: "Theater",
                column: "TheaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreening_Theater_TheaterId",
                table: "MovieScreening",
                column: "TheaterId",
                principalTable: "Theater",
                principalColumn: "TheaterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Theater_TheaterId",
                table: "Seats",
                column: "TheaterId",
                principalTable: "Theater",
                principalColumn: "TheaterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
