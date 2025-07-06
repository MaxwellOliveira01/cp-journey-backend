using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cp_journey_backend.Migrations
{
    /// <inheritdoc />
    public partial class ContestResultsAndTutorial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatementPdf",
                table: "Problems");

            migrationBuilder.AddColumn<byte[]>(
                name: "StatementsPdf",
                table: "Contests",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TutorialPdf",
                table: "Contests",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatementsPdf",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "TutorialPdf",
                table: "Contests");

            migrationBuilder.AddColumn<byte[]>(
                name: "StatementPdf",
                table: "Problems",
                type: "bytea",
                nullable: true);
        }
    }
}
