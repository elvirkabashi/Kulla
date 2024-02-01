using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KULLA.Data.Migrations
{
    public partial class InitalMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shitjet_Faturat_NrFatures",
                table: "Shitjet");

            migrationBuilder.DropIndex(
                name: "IX_Shitjet_NrFatures",
                table: "Shitjet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shitjet_NrFatures",
                table: "Shitjet",
                column: "NrFatures");

            migrationBuilder.AddForeignKey(
                name: "FK_Shitjet_Faturat_NrFatures",
                table: "Shitjet",
                column: "NrFatures",
                principalTable: "Faturat",
                principalColumn: "NrFatures",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
