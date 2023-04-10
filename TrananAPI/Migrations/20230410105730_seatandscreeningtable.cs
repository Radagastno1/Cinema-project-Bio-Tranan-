using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrananAPI.Migrations
{
    /// <inheritdoc />
    public partial class seatandscreeningtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieScreeningSeat",
                columns: table => new
                {
                    MovieScreeningsMovieScreeningId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeatsSeatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieScreeningSeat", x => new { x.MovieScreeningsMovieScreeningId, x.SeatsSeatId });
                    table.ForeignKey(
                        name: "FK_MovieScreeningSeat_MovieScreenings_MovieScreeningsMovieScreeningId",
                        column: x => x.MovieScreeningsMovieScreeningId,
                        principalTable: "MovieScreenings",
                        principalColumn: "MovieScreeningId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieScreeningSeat_Seats_SeatsSeatId",
                        column: x => x.SeatsSeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreeningSeat_SeatsSeatId",
                table: "MovieScreeningSeat",
                column: "SeatsSeatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieScreeningSeat");
        }
    }
}
