using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace divar.Migrations
{
    /// <inheritdoc />
    public partial class Experts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpertId",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    ExpertId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.ExpertId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ExpertId",
                table: "Reservations",
                column: "ExpertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Experts_ExpertId",
                table: "Reservations",
                column: "ExpertId",
                principalTable: "Experts",
                principalColumn: "ExpertId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Experts_ExpertId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ExpertId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ExpertId",
                table: "Reservations");
        }
    }
}
