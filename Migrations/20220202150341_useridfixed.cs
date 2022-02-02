using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetAPI.Migrations
{
    public partial class useridfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_AspNetUsers_UserIdId",
                table: "Alerts");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Alerts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Alerts_UserIdId",
                table: "Alerts",
                newName: "IX_Alerts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_AspNetUsers_UserId",
                table: "Alerts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_AspNetUsers_UserId",
                table: "Alerts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Alerts",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Alerts_UserId",
                table: "Alerts",
                newName: "IX_Alerts_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_AspNetUsers_UserIdId",
                table: "Alerts",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
