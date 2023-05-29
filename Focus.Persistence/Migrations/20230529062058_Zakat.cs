using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class Zakat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OnlineTerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsProceed = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyRegNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryInArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxInvoiceLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMultiUnit = table.Column<bool>(type: "bit", nullable: false),
                    IsProduction = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceWoInventory = table.Column<bool>(type: "bit", nullable: false),
                    IsArea = table.Column<bool>(type: "bit", nullable: false),
                    IsProceed = table.Column<bool>(type: "bit", nullable: false),
                    English = table.Column<bool>(type: "bit", nullable: false),
                    Arabic = table.Column<bool>(type: "bit", nullable: false),
                    CompanyNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terminal = table.Column<bool>(type: "bit", nullable: false),
                    DayStart = table.Column<bool>(type: "bit", nullable: false),
                    Step1 = table.Column<bool>(type: "bit", nullable: false),
                    Step2 = table.Column<bool>(type: "bit", nullable: false),
                    Step3 = table.Column<bool>(type: "bit", nullable: false),
                    Step4 = table.Column<bool>(type: "bit", nullable: false),
                    Step5 = table.Column<bool>(type: "bit", nullable: false),
                    TermsCondition = table.Column<bool>(type: "bit", nullable: false),
                    CashVoucher = table.Column<bool>(type: "bit", nullable: false),
                    IsOpenDay = table.Column<bool>(type: "bit", nullable: false),
                    IsTransferAllow = table.Column<bool>(type: "bit", nullable: false),
                    MasterProduct = table.Column<bool>(type: "bit", nullable: false),
                    SimpleInvoice = table.Column<bool>(type: "bit", nullable: false),
                    SoInventoryReserve = table.Column<bool>(type: "bit", nullable: false),
                    InternationalPurchase = table.Column<bool>(type: "bit", nullable: false),
                    PartiallyPurchase = table.Column<bool>(type: "bit", nullable: false),
                    VersionAllow = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseOrder = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseToGst = table.Column<bool>(type: "bit", nullable: false),
                    SaleOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoPurchaseVoucher = table.Column<bool>(type: "bit", nullable: false),
                    IsForMedical = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseAccount = table.Column<bool>(type: "bit", nullable: false),
                    SuperAdminDayStart = table.Column<bool>(type: "bit", nullable: false),
                    BankDetail = table.Column<bool>(type: "bit", nullable: false),
                    TaxInvoiceLabelAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimplifyTaxInvoiceLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimplifyTaxInvoiceLabelAr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizedPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorizedPersonCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorizedPersons_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharityResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChartiyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Business = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ContactPerson = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharityResources_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HijriDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiaryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntervalMonth = table.Column<int>(type: "int", nullable: false),
                    AmountPerMonth = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RecurringAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UgamaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "ntext", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRegister = table.Column<bool>(type: "bit", nullable: false),
                    AuthorizedPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_AuthorizedPersons_AuthorizedPersonId",
                        column: x => x.AuthorizedPersonId,
                        principalTable: "AuthorizedPersons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52E0A954-7039-483B-9224-99990743636D", "0590a33c-cd2d-4c93-9e17-fce12bc2bd9d", "Super Admin", "SUPER ADMIN" },
                    { "C1448F88-49B4-476C-B07D-33514A0F9C1E", "0590a33c-cd2d-4c93-9e17-fce19bc2bd9d", "Noble Admin", "NOBLE ADMIN" },
                    { "CEA36602-E3BD-4CF6-B186-5D8A3E558A04", "0590a33c-cd2d-4d93-9e17-fce19bc2bd9d", "Admin", "ADMIN" },
                    { "E5480E8E-A150-4C80-82AB-62B5A8EBFD1B", "1590a33c-cd2d-4c93-9e17-fce19bc2bd9d", "User", "USER" },
                    { "f9d3868c-b58c-4c55-be2b-48d564bea081", "1590a33c-cd2d-4c93-9e17-fce19bc2bd9d", "Cashier", "Cashier" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Code", "CompanyId", "ConcurrencyStamp", "Email", "EmailConfirmed", "EmployeeId", "FirstName", "ImagePath", "IsActive", "IsProceed", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OnlineTerminalId", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TerminalId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5f8d5614-2c7e-4ec0-868c-d254e6516b4d", 0, null, new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"), "117c7248-5202-44d5-a7eb-8f2717eba7e9", "noble@gmail.com", true, null, "Noble App", null, false, false, "", true, null, "noble@gmail.com", "NOBLE@GMAIL.COM", null, null, "AQAAAAEAACcQAAAAEEMJll7GgXWk1z2fatxJWFPeucssBaOZ+EXMY5MYkhRNow+oaTxB0nH+sWvTX6wKWA==", null, false, "HDM6TKME4T5DISZCHW3MHD6YLQFNSWC2", null, false, "noble@gmail.com" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AddressArabic", "AddressEnglish", "Arabic", "AutoPurchaseVoucher", "BankDetail", "Blocked", "BusinessParentId", "CashVoucher", "CategoryInArabic", "CategoryInEnglish", "CityArabic", "CityEnglish", "ClientNo", "ClientParentId", "CompanyEmail", "CompanyNameArabic", "CompanyNameEnglish", "CompanyRegNo", "CountryArabic", "CountryEnglish", "CreatedDate", "DayStart", "English", "ExpenseAccount", "ExpenseToGst", "HouseNumber", "InternationalPurchase", "InvoiceWoInventory", "IsArea", "IsForMedical", "IsMultiUnit", "IsOpenDay", "IsProceed", "IsProduction", "IsTransferAllow", "Landline", "LogoPath", "MasterProduct", "NameArabic", "NameEnglish", "ParentId", "PartiallyPurchase", "PhoneNo", "Postcode", "PurchaseOrder", "SaleOrder", "SimpleInvoice", "SimplifyTaxInvoiceLabel", "SimplifyTaxInvoiceLabelAr", "SoInventoryReserve", "Step1", "Step2", "Step3", "Step4", "Step5", "SuperAdminDayStart", "TaxInvoiceLabel", "TaxInvoiceLabelAr", "Terminal", "TermsCondition", "Town", "VatRegistrationNo", "VersionAllow", "Website" },
                values: new object[] { new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"), null, null, false, false, false, false, null, false, null, null, null, null, null, null, null, null, null, "56ty60", null, null, new DateTime(2023, 5, 29, 6, 20, 57, 674, DateTimeKind.Utc).AddTicks(9241), false, false, false, false, null, false, false, false, false, false, false, false, false, false, null, null, false, null, "noble@gmail.com", new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, false, null, false, null, null, false, false, false, false, false, false, false, null, null, false, false, null, null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Email", "noble@gmail.com", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" },
                    { 2, "CompanyId", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" },
                    { 3, "Organization", "Noble POS", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" },
                    { 4, "FullName", "Noble App", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "C1448F88-49B4-476C-B07D-33514A0F9C1E", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedPersons_CompanyId",
                table: "AuthorizedPersons",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_AuthorizedPersonId",
                table: "Beneficiaries",
                column: "AuthorizedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_CompanyId",
                table: "Beneficiaries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BenificaryNotes_BenificaryId",
                table: "BenificaryNotes",
                column: "BenificaryId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityResources_CompanyId",
                table: "CharityResources",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CompanyId",
                table: "Transactions",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BenificaryNotes");

            migrationBuilder.DropTable(
                name: "CharityResources");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "AuthorizedPersons");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
