using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class benificary_authorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvancePayment",
                table: "Beneficiaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedPaymentId",
                table: "Beneficiaries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DurationType",
                table: "Beneficiaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Beneficiaries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Beneficiaries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartMonth",
                table: "Beneficiaries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BenificaryAuthorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BenficaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorizationPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_BenificaryAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenificaryAuthorizations_ApprovalPersons_ApprovalPersonId",
                        column: x => x.ApprovalPersonId,
                        principalTable: "ApprovalPersons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BenificaryAuthorizations_AuthorizedPersons_AuthorizationPersonId",
                        column: x => x.AuthorizationPersonId,
                        principalTable: "AuthorizedPersons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BenificaryAuthorizations_Beneficiaries_BenficaryId",
                        column: x => x.BenficaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BenificaryAuthorizations_Companies_CompanyId",
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
                value: new DateTime(2023, 5, 31, 7, 59, 3, 340, DateTimeKind.Utc).AddTicks(4741));

            migrationBuilder.CreateIndex(
                name: "IX_BenificaryAuthorizations_ApprovalPersonId",
                table: "BenificaryAuthorizations",
                column: "ApprovalPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BenificaryAuthorizations_AuthorizationPersonId",
                table: "BenificaryAuthorizations",
                column: "AuthorizationPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BenificaryAuthorizations_BenficaryId",
                table: "BenificaryAuthorizations",
                column: "BenficaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BenificaryAuthorizations_CompanyId",
                table: "BenificaryAuthorizations",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenificaryAuthorizations");

            migrationBuilder.DropColumn(
                name: "AdvancePayment",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "ApprovedPaymentId",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "DurationType",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "StartMonth",
                table: "Beneficiaries");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 30, 8, 32, 25, 231, DateTimeKind.Utc).AddTicks(8252));
        }
    }
}
