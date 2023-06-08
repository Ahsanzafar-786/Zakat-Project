using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class payments_allowvoid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowVoid",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 7, 12, 34, 8, 879, DateTimeKind.Utc).AddTicks(6314));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowVoid",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 7, 10, 57, 55, 93, DateTimeKind.Utc).AddTicks(2905));
        }
    }
}
