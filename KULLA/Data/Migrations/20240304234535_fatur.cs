using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KULLA.Data.Migrations
{
    public partial class fatur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Faturat",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faturat_UserId",
                table: "Faturat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturat_AspNetUsers_UserId",
                table: "Faturat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturat_AspNetUsers_UserId",
                table: "Faturat");

            migrationBuilder.DropIndex(
                name: "IX_Faturat_UserId",
                table: "Faturat");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Faturat");
        }
    }
}
