using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cp_journey_backend.Migrations
{
    /// <inheritdoc />
    public partial class Local : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocalId",
                table: "Universities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalId",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Universities_LocalId",
                table: "Universities",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocalId",
                table: "Events",
                column: "LocalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locals_LocalId",
                table: "Events",
                column: "LocalId",
                principalTable: "Locals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Universities_Locals_LocalId",
                table: "Universities",
                column: "LocalId",
                principalTable: "Locals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locals_LocalId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Universities_Locals_LocalId",
                table: "Universities");

            migrationBuilder.DropTable(
                name: "Locals");

            migrationBuilder.DropIndex(
                name: "IX_Universities_LocalId",
                table: "Universities");

            migrationBuilder.DropIndex(
                name: "IX_Events_LocalId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocalId",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "LocalId",
                table: "Events");
        }
    }
}
