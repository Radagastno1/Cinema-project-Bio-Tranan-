using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrananAPI.Migrations
{
    /// <inheritdoc />
    public partial class deletemoviescreeningseatclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreenings_MovieScreeningId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreeningSeat_Seats_SeatId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieScreeningSeat",
                table: "MovieScreeningSeat");

            migrationBuilder.DropIndex(
                name: "IX_MovieScreeningSeat_MovieScreeningId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropColumn(
                name: "MovieScreeningSeatId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "MovieScreeningSeat");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "MovieScreeningSeat",
                newName: "ReservedSeatsSeatId");

            migrationBuilder.RenameColumn(
                name: "MovieScreeningId",
                table: "MovieScreeningSeat",
                newName: "MovieScreeningsMovieScreeningId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieScreeningSeat_SeatId",
                table: "MovieScreeningSeat",
                newName: "IX_MovieScreeningSeat_ReservedSeatsSeatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieScreeningSeat",
                table: "MovieScreeningSeat",
                columns: new[] { "MovieScreeningsMovieScreeningId", "ReservedSeatsSeatId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreenings_MovieScreeningsMovieScreeningId",
                table: "MovieScreeningSeat",
                column: "MovieScreeningsMovieScreeningId",
                principalTable: "MovieScreenings",
                principalColumn: "MovieScreeningId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreeningSeat_Seats_ReservedSeatsSeatId",
                table: "MovieScreeningSeat",
                column: "ReservedSeatsSeatId",
                principalTable: "Seats",
                principalColumn: "SeatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreenings_MovieScreeningsMovieScreeningId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieScreeningSeat_Seats_ReservedSeatsSeatId",
                table: "MovieScreeningSeat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieScreeningSeat",
                table: "MovieScreeningSeat");

            migrationBuilder.RenameColumn(
                name: "ReservedSeatsSeatId",
                table: "MovieScreeningSeat",
                newName: "SeatId");

            migrationBuilder.RenameColumn(
                name: "MovieScreeningsMovieScreeningId",
                table: "MovieScreeningSeat",
                newName: "MovieScreeningId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieScreeningSeat_ReservedSeatsSeatId",
                table: "MovieScreeningSeat",
                newName: "IX_MovieScreeningSeat_SeatId");

            migrationBuilder.AddColumn<int>(
                name: "MovieScreeningSeatId",
                table: "MovieScreeningSeat",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "MovieScreeningSeat",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieScreeningSeat",
                table: "MovieScreeningSeat",
                column: "MovieScreeningSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreeningSeat_MovieScreeningId",
                table: "MovieScreeningSeat",
                column: "MovieScreeningId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreeningSeat_MovieScreenings_MovieScreeningId",
                table: "MovieScreeningSeat",
                column: "MovieScreeningId",
                principalTable: "MovieScreenings",
                principalColumn: "MovieScreeningId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieScreeningSeat_Seats_SeatId",
                table: "MovieScreeningSeat",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "SeatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
