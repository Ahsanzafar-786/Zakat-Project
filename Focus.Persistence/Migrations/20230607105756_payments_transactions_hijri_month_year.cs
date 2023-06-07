using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class payments_transactions_hijri_month_year : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HijriMonth",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HijriYear",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HijriMonth",
                table: "CharityTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HijriYear",
                table: "CharityTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVoid",
                table: "CharityTransaction",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 7, 10, 57, 55, 93, DateTimeKind.Utc).AddTicks(2905));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HijriMonth",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "HijriYear",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "HijriMonth",
                table: "CharityTransaction");

            migrationBuilder.DropColumn(
                name: "HijriYear",
                table: "CharityTransaction");

            migrationBuilder.DropColumn(
                name: "IsVoid",
                table: "CharityTransaction");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 7, 10, 45, 54, 553, DateTimeKind.Utc).AddTicks(6583));
        }
    }
}
