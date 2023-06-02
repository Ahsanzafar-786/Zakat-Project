using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class funds_code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Funds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 2, 10, 54, 18, 927, DateTimeKind.Utc).AddTicks(3167));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Funds");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 2, 6, 25, 36, 313, DateTimeKind.Utc).AddTicks(4111));
        }
    }
}
