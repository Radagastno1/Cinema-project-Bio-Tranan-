using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE.Migrations
{
    /// <inheritdoc />
    public partial class reservationcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationCode",
                table: "Reviews",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationCode",
                table: "Reviews");
        }
    }
}
