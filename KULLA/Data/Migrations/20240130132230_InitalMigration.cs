using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KULLA.Data.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faturat",
                columns: table => new
                {
                    NrFatures = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Blersi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Totali = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturat", x => x.NrFatures);
                });

            migrationBuilder.CreateTable(
                name: "Produktet",
                columns: table => new
                {
                    ProduktiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cmimi = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produktet", x => x.ProduktiId);
                });

            migrationBuilder.CreateTable(
                name: "Shitjet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduktiId = table.Column<int>(type: "int", nullable: false),
                    Sasia = table.Column<int>(type: "int", nullable: false),
                    Blersi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrFatures = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shitjet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shitjet_Faturat_NrFatures",
                        column: x => x.NrFatures,
                        principalTable: "Faturat",
                        principalColumn: "NrFatures",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shitjet_Produktet_ProduktiId",
                        column: x => x.ProduktiId,
                        principalTable: "Produktet",
                        principalColumn: "ProduktiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shitjet_NrFatures",
                table: "Shitjet",
                column: "NrFatures");

            migrationBuilder.CreateIndex(
                name: "IX_Shitjet_ProduktiId",
                table: "Shitjet",
                column: "ProduktiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shitjet");

            migrationBuilder.DropTable(
                name: "Faturat");

            migrationBuilder.DropTable(
                name: "Produktet");
        }
    }
}
