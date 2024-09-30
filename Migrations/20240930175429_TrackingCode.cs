using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace divar.Migrations
{
    /// <inheritdoc />
    public partial class TrackingCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpertReviewResult",
                table: "Reservations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingCode",
                table: "Reservations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpertReviewResult",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TrackingCode",
                table: "Reservations");
        }
    }
}
