using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE.Migrations
{
    /// <inheritdoc />
    public partial class moviescreenings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreening_Movies_MovieId",
                table: "MovieScreening");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreening_Theaters_TheaterId",
                table: "MovieScreening");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreening_MovieScreeningsMovieScreeningId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_MovieScreening_MovieScreeningId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieScreening",
                table: "MovieScreening");

            migrationBuilder.RenameTable(
                name: "MovieScreening",
                newName: "MovieScreenings");

            migrationBuilder.RenameIndex(
                name: "IX_MovieScreening_TheaterId",
                table: "MovieScreenings",
                newName: "IX_MovieScreenings_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieScreening_MovieId",
                table: "MovieScreenings",
                newName: "IX_MovieScreenings_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieScreenings",
                table: "MovieScreenings",
                column: "MovieScreeningId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreenings_Movies_MovieId",
                table: "MovieScreenings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreenings_Theaters_TheaterId",
                table: "MovieScreenings",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "TheaterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreenings_MovieScreeningsMovieScreeningId",
                table: "MovieScreeningSeat",
                column: "MovieScreeningsMovieScreeningId",
                principalTable: "MovieScreenings",
                principalColumn: "MovieScreeningId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_MovieScreenings_MovieScreeningId",
                table: "Reservation",
                column: "MovieScreeningId",
                principalTable: "MovieScreenings",
                principalColumn: "MovieScreeningId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreenings_Movies_MovieId",
                table: "MovieScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreenings_Theaters_TheaterId",
                table: "MovieScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreenings_MovieScreeningsMovieScreeningId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_MovieScreenings_MovieScreeningId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieScreenings",
                table: "MovieScreenings");

            migrationBuilder.RenameTable(
                name: "MovieScreenings",
                newName: "MovieScreening");

            migrationBuilder.RenameIndex(
                name: "IX_MovieScreenings_TheaterId",
                table: "MovieScreening",
                newName: "IX_MovieScreening_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieScreenings_MovieId",
                table: "MovieScreening",
                newName: "IX_MovieScreening_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieScreening",
                table: "MovieScreening",
                column: "MovieScreeningId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreening_Movies_MovieId",
                table: "MovieScreening",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreening_Theaters_TheaterId",
                table: "MovieScreening",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "TheaterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreening_MovieScreeningsMovieScreeningId",
                table: "MovieScreeningSeat",
                column: "MovieScreeningsMovieScreeningId",
                principalTable: "MovieScreening",
                principalColumn: "MovieScreeningId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_MovieScreening_MovieScreeningId",
                table: "Reservation",
                column: "MovieScreeningId",
                principalTable: "MovieScreening",
                principalColumn: "MovieScreeningId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
