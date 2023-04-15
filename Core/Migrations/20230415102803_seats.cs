using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE.Migrations
{
    /// <inheritdoc />
    public partial class seats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Theater",
                columns: table => new
                {
                    TheaterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Rows = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxAmountAvailebleSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    TheaterPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theater", x => x.TheaterId);
                });

            migrationBuilder.CreateTable(
                name: "MovieScreening",
                columns: table => new
                {
                    MovieScreeningId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateAndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerPerson = table.Column<decimal>(type: "TEXT", nullable: false),
                    TheaterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieScreening", x => x.MovieScreeningId);
                    table.ForeignKey(
                        name: "FK_MovieScreening_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieScreening_Theater_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "Theater",
                        principalColumn: "TheaterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeatNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Row = table.Column<int>(type: "INTEGER", nullable: false),
                    IsWheelChairSpace = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBooked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsNotBookable = table.Column<bool>(type: "INTEGER", nullable: false),
                    TheaterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Theater_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "Theater",
                        principalColumn: "TheaterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReservationCode = table.Column<int>(type: "INTEGER", nullable: false),
                    MovieScreeningId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_MovieScreening_MovieScreeningId",
                        column: x => x.MovieScreeningId,
                        principalTable: "MovieScreening",
                        principalColumn: "MovieScreeningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieScreeningSeat",
                columns: table => new
                {
                    MovieScreeningsMovieScreeningId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservedSeatsSeatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieScreeningSeat", x => new { x.MovieScreeningsMovieScreeningId, x.ReservedSeatsSeatId });
                    table.ForeignKey(
                        name: "FK_MovieScreeningSeat_MovieScreening_MovieScreeningsMovieScreeningId",
                        column: x => x.MovieScreeningsMovieScreeningId,
                        principalTable: "MovieScreening",
                        principalColumn: "MovieScreeningId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieScreeningSeat_Seats_ReservedSeatsSeatId",
                        column: x => x.ReservedSeatsSeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationSeat",
                columns: table => new
                {
                    ReservationsReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeatsSeatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSeat", x => new { x.ReservationsReservationId, x.SeatsSeatId });
                    table.ForeignKey(
                        name: "FK_ReservationSeat_Reservation_ReservationsReservationId",
                        column: x => x.ReservationsReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationSeat_Seats_SeatsSeatId",
                        column: x => x.SeatsSeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreening_MovieId",
                table: "MovieScreening",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreening_TheaterId",
                table: "MovieScreening",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreeningSeat_ReservedSeatsSeatId",
                table: "MovieScreeningSeat",
                column: "ReservedSeatsSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_MovieScreeningId",
                table: "Reservation",
                column: "MovieScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSeat_SeatsSeatId",
                table: "ReservationSeat",
                column: "SeatsSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TheaterId",
                table: "Seats",
                column: "TheaterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieScreeningSeat");

            migrationBuilder.DropTable(
                name: "ReservationSeat");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "MovieScreening");

            migrationBuilder.DropTable(
                name: "Theater");
        }
    }
}
