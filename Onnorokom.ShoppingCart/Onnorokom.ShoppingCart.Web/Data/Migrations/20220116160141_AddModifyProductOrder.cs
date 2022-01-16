using Microsoft.EntityFrameworkCore.Migrations;

namespace Onnorokom.ShoppingCart.Web.Data.Migrations
{
    public partial class AddModifyProductOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductOrders");
        }
    }
}
