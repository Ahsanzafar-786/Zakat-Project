using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class paymentsconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Beneficiaries_BeneficiariesId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BeneficiariesId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BeneficiariesId",
                table: "Payments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 31, 11, 29, 4, 610, DateTimeKind.Utc).AddTicks(5952));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BenificayId",
                table: "Payments",
                column: "BenificayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Beneficiaries_BenificayId",
                table: "Payments",
                column: "BenificayId",
                principalTable: "Beneficiaries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Beneficiaries_BenificayId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BenificayId",
                table: "Payments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<Guid>(
                name: "BeneficiariesId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 31, 11, 25, 58, 970, DateTimeKind.Utc).AddTicks(7851));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BeneficiariesId",
                table: "Payments",
                column: "BeneficiariesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Beneficiaries_BeneficiariesId",
                table: "Payments",
                column: "BeneficiariesId",
                principalTable: "Beneficiaries",
                principalColumn: "Id");
        }
    }
}
