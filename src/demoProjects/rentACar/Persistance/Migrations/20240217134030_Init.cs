using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

//بسم الله الرحمن الرحيم
/**
 *
 * @author Huseyin_Aydin
 * @since 1994
 * @category DotNet Core nArchitechture, C#.
 *
 */

namespace Persistance.Migrations;

public partial class Init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Brands",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Brands", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Model",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BrandId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DailyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Model", x => x.Id);
                table.ForeignKey(
                    name: "FK_Model_Brands_BrandId",
                    column: x => x.BrandId,
                    principalTable: "Brands",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Brands",
            columns: new[] { "Id", "Name" },
            values: new object[] { 1, "Renault" });

        migrationBuilder.InsertData(
            table: "Brands",
            columns: new[] { "Id", "Name" },
            values: new object[] { 2, "Tofaş" });

        migrationBuilder.CreateIndex(
            name: "IX_Model_BrandId",
            table: "Model",
            column: "BrandId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Model");

        migrationBuilder.DropTable(
            name: "Brands");
    }
}
