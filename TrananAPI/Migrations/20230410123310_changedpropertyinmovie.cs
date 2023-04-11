using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrananAPI.Migrations
{
    /// <inheritdoc />
    public partial class changedpropertyinmovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationSeconds",
                table: "Movies",
                newName: "DurationMinutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationMinutes",
                table: "Movies",
                newName: "DurationSeconds");
        }
    }
}
