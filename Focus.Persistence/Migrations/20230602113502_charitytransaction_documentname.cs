using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class charitytransaction_documentname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "CharityTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 2, 11, 35, 1, 633, DateTimeKind.Utc).AddTicks(5349));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "CharityTransaction");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 2, 10, 54, 18, 927, DateTimeKind.Utc).AddTicks(3167));
        }
    }
}
