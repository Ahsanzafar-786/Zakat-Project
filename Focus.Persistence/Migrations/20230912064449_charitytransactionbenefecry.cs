using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class charitytransactionbenefecry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 9, 12, 6, 44, 48, 287, DateTimeKind.Utc).AddTicks(8649));

            migrationBuilder.CreateIndex(
                name: "IX_CharityTransaction_BenificayId",
                table: "CharityTransaction",
                column: "BenificayId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharityTransaction_Beneficiaries_BenificayId",
                table: "CharityTransaction",
                column: "BenificayId",
                principalTable: "Beneficiaries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharityTransaction_Beneficiaries_BenificayId",
                table: "CharityTransaction");

            migrationBuilder.DropIndex(
                name: "IX_CharityTransaction_BenificayId",
                table: "CharityTransaction");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 9, 8, 7, 34, 3, 374, DateTimeKind.Utc).AddTicks(7693));
        }
    }
}
