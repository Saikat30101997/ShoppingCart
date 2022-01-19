using Microsoft.EntityFrameworkCore.Migrations;

namespace Onnorokom.ShoppingCart.Web.Migrations.ShoppingCartDb
{
    public partial class Addrelationwitthorderandcarts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_UserId",
                table: "ProductOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_AspNetUsers_UserId",
                table: "ProductOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_AspNetUsers_UserId",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_UserId",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");
        }
    }
}
