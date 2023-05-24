﻿// <auto-generated />
using System;
using Focus.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Focus.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Focus.Business.Users.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProceed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid?>("OnlineTerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TerminalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                            AccessFailedCount = 0,
                            CompanyId = new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                            ConcurrencyStamp = "117c7248-5202-44d5-a7eb-8f2717eba7e9",
                            Email = "noble@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Noble App",
                            IsActive = false,
                            IsProceed = false,
                            LastName = "",
                            LockoutEnabled = true,
                            NormalizedEmail = "noble@gmail.com",
                            NormalizedUserName = "NOBLE@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEEMJll7GgXWk1z2fatxJWFPeucssBaOZ+EXMY5MYkhRNow+oaTxB0nH+sWvTX6wKWA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "HDM6TKME4T5DISZCHW3MHD6YLQFNSWC2",
                            TwoFactorEnabled = false,
                            UserName = "noble@gmail.com"
                        });
                });

            modelBuilder.Entity("Focus.Domain.Entities.Color", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Focus.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Arabic")
                        .HasColumnType("bit");

                    b.Property<bool>("AutoPurchaseVoucher")
                        .HasColumnType("bit");

                    b.Property<bool>("BankDetail")
                        .HasColumnType("bit");

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<Guid?>("BusinessParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CashVoucher")
                        .HasColumnType("bit");

                    b.Property<string>("CategoryInArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryInEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ClientParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyNameArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyNameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyRegNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DayStart")
                        .HasColumnType("bit");

                    b.Property<bool>("English")
                        .HasColumnType("bit");

                    b.Property<bool>("ExpenseAccount")
                        .HasColumnType("bit");

                    b.Property<bool>("ExpenseToGst")
                        .HasColumnType("bit");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InternationalPurchase")
                        .HasColumnType("bit");

                    b.Property<bool>("InvoiceWoInventory")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArea")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForMedical")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMultiUnit")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpenDay")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProceed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProduction")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTransferAllow")
                        .HasColumnType("bit");

                    b.Property<string>("Landline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MasterProduct")
                        .HasColumnType("bit");

                    b.Property<string>("NameArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("PartiallyPurchase")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PurchaseOrder")
                        .HasColumnType("bit");

                    b.Property<string>("SaleOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SimpleInvoice")
                        .HasColumnType("bit");

                    b.Property<string>("SimplifyTaxInvoiceLabel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SimplifyTaxInvoiceLabelAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoInventoryReserve")
                        .HasColumnType("bit");

                    b.Property<bool>("Step1")
                        .HasColumnType("bit");

                    b.Property<bool>("Step2")
                        .HasColumnType("bit");

                    b.Property<bool>("Step3")
                        .HasColumnType("bit");

                    b.Property<bool>("Step4")
                        .HasColumnType("bit");

                    b.Property<bool>("Step5")
                        .HasColumnType("bit");

                    b.Property<bool>("SuperAdminDayStart")
                        .HasColumnType("bit");

                    b.Property<string>("TaxInvoiceLabel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxInvoiceLabelAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Terminal")
                        .HasColumnType("bit");

                    b.Property<bool>("TermsCondition")
                        .HasColumnType("bit");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VatRegistrationNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("VersionAllow")
                        .HasColumnType("bit");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                            Arabic = false,
                            AutoPurchaseVoucher = false,
                            BankDetail = false,
                            Blocked = false,
                            CashVoucher = false,
                            CompanyRegNo = "56ty60",
                            CreatedDate = new DateTime(2023, 5, 24, 8, 5, 42, 200, DateTimeKind.Utc).AddTicks(7602),
                            DayStart = false,
                            English = false,
                            ExpenseAccount = false,
                            ExpenseToGst = false,
                            InternationalPurchase = false,
                            InvoiceWoInventory = false,
                            IsArea = false,
                            IsForMedical = false,
                            IsMultiUnit = false,
                            IsOpenDay = false,
                            IsProceed = false,
                            IsProduction = false,
                            IsTransferAllow = false,
                            MasterProduct = false,
                            NameEnglish = "noble@gmail.com",
                            ParentId = new Guid("00000000-0000-0000-0000-000000000000"),
                            PartiallyPurchase = false,
                            PurchaseOrder = false,
                            SimpleInvoice = false,
                            SoInventoryReserve = false,
                            Step1 = false,
                            Step2 = false,
                            Step3 = false,
                            Step4 = false,
                            Step5 = false,
                            SuperAdminDayStart = false,
                            Terminal = false,
                            TermsCondition = false,
                            VersionAllow = false
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "C1448F88-49B4-476C-B07D-33514A0F9C1E",
                            ConcurrencyStamp = "0590a33c-cd2d-4c93-9e17-fce19bc2bd9d",
                            Name = "Noble Admin",
                            NormalizedName = "NOBLE ADMIN"
                        },
                        new
                        {
                            Id = "52E0A954-7039-483B-9224-99990743636D",
                            ConcurrencyStamp = "0590a33c-cd2d-4c93-9e17-fce12bc2bd9d",
                            Name = "Super Admin",
                            NormalizedName = "SUPER ADMIN"
                        },
                        new
                        {
                            Id = "CEA36602-E3BD-4CF6-B186-5D8A3E558A04",
                            ConcurrencyStamp = "0590a33c-cd2d-4d93-9e17-fce19bc2bd9d",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "E5480E8E-A150-4C80-82AB-62B5A8EBFD1B",
                            ConcurrencyStamp = "1590a33c-cd2d-4c93-9e17-fce19bc2bd9d",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "Email",
                            ClaimValue = "noble@gmail.com",
                            UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "CompanyId",
                            ClaimValue = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                            UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "Organization",
                            ClaimValue = "Noble POS",
                            UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "FullName",
                            ClaimValue = "Noble App",
                            UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                            RoleId = "C1448F88-49B4-476C-B07D-33514A0F9C1E"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Focus.Domain.Entities.Color", b =>
                {
                    b.HasOne("Focus.Domain.Entities.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Focus.Business.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Focus.Business.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Focus.Business.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Focus.Business.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
