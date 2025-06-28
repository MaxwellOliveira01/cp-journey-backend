using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cp_journey_backend.Migrations
{
    /// <inheritdoc />
    public partial class Person : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Profiles_ProfileId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "TeamMembers",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_ProfileId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_PersonId");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Handle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    UniversityId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UniversityId",
                table: "Persons",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Persons_PersonId",
                table: "TeamMembers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Persons_PersonId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "TeamMembers",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_PersonId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_ProfileId");

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UniversityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Handle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_TeamId",
                table: "Profiles",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniversityId",
                table: "Profiles",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Profiles_ProfileId",
                table: "TeamMembers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
