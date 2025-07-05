using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cp_journey_backend.Migrations
{
    /// <inheritdoc />
    public partial class TeamResultAndSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Problems_ProblemId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_TeamResult_TeamResultId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResult_Contests_ContestId",
                table: "TeamResult");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResult_Teams_TeamId",
                table: "TeamResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamResult",
                table: "TeamResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.RenameTable(
                name: "TeamResult",
                newName: "TeamResults");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResult_TeamId",
                table: "TeamResults",
                newName: "IX_TeamResults_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResult_ContestId",
                table: "TeamResults",
                newName: "IX_TeamResults_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_TeamResultId",
                table: "Submissions",
                newName: "IX_Submissions_TeamResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_ProblemId",
                table: "Submissions",
                newName: "IX_Submissions_ProblemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamResults",
                table: "TeamResults",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Problems_ProblemId",
                table: "Submissions",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_TeamResults_TeamResultId",
                table: "Submissions",
                column: "TeamResultId",
                principalTable: "TeamResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResults_Contests_ContestId",
                table: "TeamResults",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResults_Teams_TeamId",
                table: "TeamResults",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Problems_ProblemId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_TeamResults_TeamResultId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResults_Contests_ContestId",
                table: "TeamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResults_Teams_TeamId",
                table: "TeamResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamResults",
                table: "TeamResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.RenameTable(
                name: "TeamResults",
                newName: "TeamResult");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResults_TeamId",
                table: "TeamResult",
                newName: "IX_TeamResult_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResults_ContestId",
                table: "TeamResult",
                newName: "IX_TeamResult_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_TeamResultId",
                table: "Submission",
                newName: "IX_Submission_TeamResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_ProblemId",
                table: "Submission",
                newName: "IX_Submission_ProblemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamResult",
                table: "TeamResult",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Problems_ProblemId",
                table: "Submission",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_TeamResult_TeamResultId",
                table: "Submission",
                column: "TeamResultId",
                principalTable: "TeamResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResult_Contests_ContestId",
                table: "TeamResult",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResult_Teams_TeamId",
                table: "TeamResult",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
