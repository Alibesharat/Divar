using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace divar.Migrations
{
    /// <inheritdoc />
    public partial class ReviewStatuse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostToken",
                table: "Reservations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewStatus",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostToken",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "Reservations");
        }
    }
}
