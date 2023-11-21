using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class PaymentAuthorizePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentAuthorizePersonId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentByAuthorizePerson",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentByAuthorizePerson",
                table: "CharityTransaction",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PaymentAuthorizePersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizePersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAuthorizePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAuthorizePersons_AuthorizedPersons_AuthorizePersonId",
                        column: x => x.AuthorizePersonId,
                        principalTable: "AuthorizedPersons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentAuthorizePersons_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAuthorizePersons_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentAuthorizePersonItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentAuthorizePersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BenificayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAuthorizePersonItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAuthorizePersonItems_Beneficiaries_BenificayId",
                        column: x => x.BenificayId,
                        principalTable: "Beneficiaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentAuthorizePersonItems_PaymentAuthorizePersons_PaymentAuthorizePersonId",
                        column: x => x.PaymentAuthorizePersonId,
                        principalTable: "PaymentAuthorizePersons",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 11, 21, 10, 48, 45, 550, DateTimeKind.Utc).AddTicks(370));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentAuthorizePersonId",
                table: "Payments",
                column: "PaymentAuthorizePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAuthorizePersonItems_BenificayId",
                table: "PaymentAuthorizePersonItems",
                column: "BenificayId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAuthorizePersonItems_PaymentAuthorizePersonId",
                table: "PaymentAuthorizePersonItems",
                column: "PaymentAuthorizePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAuthorizePersons_AuthorizePersonId",
                table: "PaymentAuthorizePersons",
                column: "AuthorizePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAuthorizePersons_CompanyId",
                table: "PaymentAuthorizePersons",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAuthorizePersons_PaymentId",
                table: "PaymentAuthorizePersons",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentAuthorizePersons_PaymentAuthorizePersonId",
                table: "Payments",
                column: "PaymentAuthorizePersonId",
                principalTable: "PaymentAuthorizePersons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentAuthorizePersons_PaymentAuthorizePersonId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentAuthorizePersonItems");

            migrationBuilder.DropTable(
                name: "PaymentAuthorizePersons");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentAuthorizePersonId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentAuthorizePersonId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentByAuthorizePerson",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentByAuthorizePerson",
                table: "CharityTransaction");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 9, 20, 8, 19, 35, 366, DateTimeKind.Utc).AddTicks(3912));
        }
    }
}
