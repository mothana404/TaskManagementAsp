using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementServer.Migrations
{
    /// <inheritdoc />
    public partial class addingTeamRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamRequest_Teams_TeamId",
                table: "TeamRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamRequest_Users_UserId",
                table: "TeamRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamRequest",
                table: "TeamRequest");

            migrationBuilder.RenameTable(
                name: "TeamRequest",
                newName: "TeamRequests");

            migrationBuilder.RenameIndex(
                name: "IX_TeamRequest_UserId",
                table: "TeamRequests",
                newName: "IX_TeamRequests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamRequest_TeamId",
                table: "TeamRequests",
                newName: "IX_TeamRequests_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamRequests",
                table: "TeamRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamRequests_Teams_TeamId",
                table: "TeamRequests",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamRequests_Users_UserId",
                table: "TeamRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamRequests_Teams_TeamId",
                table: "TeamRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamRequests_Users_UserId",
                table: "TeamRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamRequests",
                table: "TeamRequests");

            migrationBuilder.RenameTable(
                name: "TeamRequests",
                newName: "TeamRequest");

            migrationBuilder.RenameIndex(
                name: "IX_TeamRequests_UserId",
                table: "TeamRequest",
                newName: "IX_TeamRequest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamRequests_TeamId",
                table: "TeamRequest",
                newName: "IX_TeamRequest_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamRequest",
                table: "TeamRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamRequest_Teams_TeamId",
                table: "TeamRequest",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamRequest_Users_UserId",
                table: "TeamRequest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
