using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Errors_Users_UserId",
                table: "Errors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Errors",
                table: "Errors");

            migrationBuilder.RenameTable(
                name: "Errors",
                newName: "ErrorLogs");

            migrationBuilder.RenameIndex(
                name: "IX_Errors_UserId",
                table: "ErrorLogs",
                newName: "IX_ErrorLogs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorLogs",
                table: "ErrorLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_Users_UserId",
                table: "ErrorLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_Users_UserId",
                table: "ErrorLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorLogs",
                table: "ErrorLogs");

            migrationBuilder.RenameTable(
                name: "ErrorLogs",
                newName: "Errors");

            migrationBuilder.RenameIndex(
                name: "IX_ErrorLogs_UserId",
                table: "Errors",
                newName: "IX_Errors_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Errors",
                table: "Errors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Errors_Users_UserId",
                table: "Errors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
