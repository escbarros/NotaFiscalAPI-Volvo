using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiNotaFiscal.Migrations
{
    public partial class AddVersao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VersaoDoSistema",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VersaoDoSistema",
                table: "Produtos");
        }
    }
}
