using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KULLA.Data.Migrations
{
    public partial class FaturaAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "Faturat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Destinacjoni",
                table: "Faturat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "Faturat");

            migrationBuilder.DropColumn(
                name: "Destinacjoni",
                table: "Faturat");
        }
    }
}
