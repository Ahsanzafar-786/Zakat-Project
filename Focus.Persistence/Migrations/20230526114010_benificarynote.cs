using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class benificarynote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenificaryNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "ntext", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BenificaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenificaryNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenificaryNotes_Beneficiaries_BenificaryId",
                        column: x => x.BenificaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 26, 11, 40, 9, 644, DateTimeKind.Utc).AddTicks(4910));

            migrationBuilder.CreateIndex(
                name: "IX_BenificaryNotes_BenificaryId",
                table: "BenificaryNotes",
                column: "BenificaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenificaryNotes");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "CreatedDate",
                value: new DateTime(2023, 5, 26, 7, 42, 50, 423, DateTimeKind.Utc).AddTicks(612));
        }
    }
}
