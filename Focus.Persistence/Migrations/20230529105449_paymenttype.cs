using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class paymenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApprovalPersonId",
                table: "Beneficiaries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Beneficiaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "Beneficiaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTypeId",
                table: "Beneficiaries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 29, 10, 54, 48, 363, DateTimeKind.Utc).AddTicks(8905));

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_ApprovalPersonId",
                table: "Beneficiaries",
                column: "ApprovalPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_PaymentTypeId",
                table: "Beneficiaries",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_CompanyId",
                table: "PaymentTypes",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_ApprovalPersons_ApprovalPersonId",
                table: "Beneficiaries",
                column: "ApprovalPersonId",
                principalTable: "ApprovalPersons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_PaymentTypes_PaymentTypeId",
                table: "Beneficiaries",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_ApprovalPersons_ApprovalPersonId",
                table: "Beneficiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_PaymentTypes_PaymentTypeId",
                table: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaries_ApprovalPersonId",
                table: "Beneficiaries");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaries_PaymentTypeId",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "ApprovalPersonId",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Beneficiaries");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 29, 7, 46, 2, 873, DateTimeKind.Utc).AddTicks(3022));
        }
    }
}
