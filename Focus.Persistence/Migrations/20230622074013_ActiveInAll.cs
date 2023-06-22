using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class ActiveInAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BenificaryNotes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Beneficiaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AuthorizedPersons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatus",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ApprovalPersons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 22, 7, 40, 12, 0, DateTimeKind.Utc).AddTicks(2467));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BenificaryNotes");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AuthorizedPersons");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ApprovalPersons");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 6, 9, 7, 34, 14, 579, DateTimeKind.Utc).AddTicks(6175));
        }
    }
}
