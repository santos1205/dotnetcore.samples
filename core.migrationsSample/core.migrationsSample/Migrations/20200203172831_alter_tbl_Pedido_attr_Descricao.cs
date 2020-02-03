using Microsoft.EntityFrameworkCore.Migrations;

namespace core.migrationsSample.Migrations
{
    public partial class alter_tbl_Pedido_attr_Descricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Pedidos",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Pedidos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }
    }
}
