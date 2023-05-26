using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class authorizeperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorizedPersonId",
                table: "Beneficiaries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RecurringAmount",
                table: "Beneficiaries",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AuthorizedPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorizedPersonCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedPersons", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 26, 7, 42, 50, 423, DateTimeKind.Utc).AddTicks(612));

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_AuthorizedPersonId",
                table: "Beneficiaries",
                column: "AuthorizedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_AuthorizedPersons_AuthorizedPersonId",
                table: "Beneficiaries",
                column: "AuthorizedPersonId",
                principalTable: "AuthorizedPersons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_AuthorizedPersons_AuthorizedPersonId",
                table: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "AuthorizedPersons");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaries_AuthorizedPersonId",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "AuthorizedPersonId",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "RecurringAmount",
                table: "Beneficiaries");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 25, 10, 26, 36, 591, DateTimeKind.Utc).AddTicks(2111));
        }
    }
}
