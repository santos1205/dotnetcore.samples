using Microsoft.EntityFrameworkCore.Migrations;

namespace Oauth_sln.Migrations
{
    public partial class saboresData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sabores",
                columns: new[] { "Id", "Descricao", "Valor" },
                values: new object[] { 1, "Castanha", null });

            migrationBuilder.InsertData(
                table: "Sabores",
                columns: new[] { "Id", "Descricao", "Valor" },
                values: new object[] { 2, "Chocolate", null });

            migrationBuilder.InsertData(
                table: "Sabores",
                columns: new[] { "Id", "Descricao", "Valor" },
                values: new object[] { 3, "Casadinho (goiaba)", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sabores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sabores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sabores",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
