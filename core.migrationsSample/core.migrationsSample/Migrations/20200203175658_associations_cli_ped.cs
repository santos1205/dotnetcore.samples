using Microsoft.EntityFrameworkCore.Migrations;

namespace core.migrationsSample.Migrations
{
    public partial class associations_cli_ped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteIdCliente",
                table: "Pedidos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pedido_IdCliente",
                table: "Pedidos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteIdCliente",
                table: "Pedidos",
                column: "ClienteIdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteIdCliente",
                table: "Pedidos",
                column: "ClienteIdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteIdCliente",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ClienteIdCliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClienteIdCliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Pedido_IdCliente",
                table: "Pedidos");
        }
    }
}
