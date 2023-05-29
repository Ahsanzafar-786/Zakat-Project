using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class update_benificary_authorizeperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Beneficiaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AuthorizedPersons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IqamaNo",
                table: "AuthorizedPersons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AuthorizedPersons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "AuthorizedPersons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 29, 11, 29, 30, 704, DateTimeKind.Utc).AddTicks(9813));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AuthorizedPersons");

            migrationBuilder.DropColumn(
                name: "IqamaNo",
                table: "AuthorizedPersons");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AuthorizedPersons");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "AuthorizedPersons");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 29, 10, 54, 48, 363, DateTimeKind.Utc).AddTicks(8905));
        }
    }
}
