using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onnorokom.ShoppingCart.Web.Data.Migrations
{
    public partial class AddDataSeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("bf5d7a2b-fc20-4f79-bd41-1c42aaa8c509"), "f8aaef9e-b63b-4ad0-a562-1a5461e126ce", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("16b844ad-4c07-46b0-bfae-343f0f0bd397"), "e9285ea0-3ecf-472b-ac01-9e662b8b9a65", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("16b844ad-4c07-46b0-bfae-343f0f0bd397"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bf5d7a2b-fc20-4f79-bd41-1c42aaa8c509"));
        }
    }
}
