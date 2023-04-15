using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE.Migrations
{
    /// <inheritdoc />
    public partial class trailerid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrailerId",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerId",
                table: "Movies");
        }
    }
}
