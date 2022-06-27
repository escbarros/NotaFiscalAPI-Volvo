using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiNotaFiscal.Migrations
{
    public partial class Atualizar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCompra",
                table: "Venda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCompra",
                table: "Venda",
                type: "int",
                nullable: true);
        }
    }
}
