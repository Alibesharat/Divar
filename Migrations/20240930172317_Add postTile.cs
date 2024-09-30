using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace divar.Migrations
{
    /// <inheritdoc />
    public partial class AddpostTile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostTitle",
                table: "Reservations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostTitle",
                table: "Reservations");
        }
    }
}
