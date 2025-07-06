using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cp_journey_backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_Persons_SetterId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_SetterId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "SetterId",
                table: "Problems");

            migrationBuilder.AlterColumn<string>(
                name: "Handle",
                table: "Persons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetterId",
                table: "Problems",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Handle",
                table: "Persons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problems_SetterId",
                table: "Problems",
                column: "SetterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_Persons_SetterId",
                table: "Problems",
                column: "SetterId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
