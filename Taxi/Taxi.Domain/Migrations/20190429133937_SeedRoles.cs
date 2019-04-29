using Microsoft.EntityFrameworkCore.Migrations;

namespace Taxi.Domain.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51e233ee-1852-47f4-9a3a-b75a7e4b38f3", "6602c6f9-b76a-4e15-a460-f5444362d2c7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f04c848d-a396-499e-a853-831b50fb2460", "c96ac1c3-f257-4a37-ae40-b3f2e9eb082d", "Driver", "DRIVER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "11cf777f-44ad-4e69-8e13-7727411ee5db", "ceb5b3dc-ead7-4eb0-9be6-72c1e95f7c26", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11cf777f-44ad-4e69-8e13-7727411ee5db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51e233ee-1852-47f4-9a3a-b75a7e4b38f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f04c848d-a396-499e-a853-831b50fb2460");
        }
    }
}
