using Microsoft.EntityFrameworkCore.Migrations;

namespace core.migrationsSample.Migrations
{
    public partial class alterTblAtributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Clientes");
        }
    }
}
