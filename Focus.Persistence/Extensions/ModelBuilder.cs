using System;
using System.Linq;
using System.Reflection;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Focus.Persistence.Extensions
{
    public static class ModelBuilder
    {
        public static void ShadowProperties(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;

                // set auditing properties
                if (typeof(IAuditedEntityBase).IsAssignableFrom(t))
                {
                    var method = SetAuditingShadowPropertiesMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set tenant properties
                if (typeof(ITenant).IsAssignableFrom(t))
                {
                    var method = SetTenantShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set soft delete property
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    var method = SetIsDeletedShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set concurrency token property
                if (typeof(IConcurrencyCheckEntity).IsAssignableFrom(t))
                {
                    var method = SetConcurrencyStampShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set concurrency token property
                if (typeof(IRecordDailyEntry).IsAssignableFrom(t))
                {
                    var method = SetDailyEntryShadowPropertiesMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }
            }
        }

        private static readonly MethodInfo SetIsDeletedShadowPropertyMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetIsDeletedShadowProperty");

        private static readonly MethodInfo SetTenantShadowPropertyMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetTenantShadowProperty");

        private static readonly MethodInfo SetAuditingShadowPropertiesMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetAuditingShadowProperties");

        private static readonly MethodInfo SetConcurrencyStampShadowPropertyMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetConcurrencyShadowProperty");

        private static readonly MethodInfo SetDailyEntryShadowPropertiesMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetDailyEntryShadowProperties");

        public static void SetIsDeletedShadowProperty<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, ISoftDelete
        {
            // define shadow property
            builder.Entity<T>().Property<bool>("IsDeleted");
        }

        public static void SetTenantShadowProperty<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, ITenant
        {
            // define shadow property
            builder.Entity<T>().Property<Guid>("CompanyId");
            // define FK to Company
            builder.Entity<T>().HasOne<Company>().WithMany().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.Restrict);
        }

        public static void SetAuditingShadowProperties<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, IAuditedEntityBase
        {
            // define shadow properties
            builder.Entity<T>().Property<DateTime>("CreatedOn").HasDefaultValueSql("GetUtcDate()");
            builder.Entity<T>().Property<DateTime>("ModifiedOn").HasDefaultValueSql("GetUtcDate()");
            builder.Entity<T>().Property<string>("CreatedById");
            builder.Entity<T>().Property<string>("ModifiedById");

            // define FKs to User
            //builder.Entity<T>().HasOne<User>().WithMany().HasForeignKey("CreatedById").OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<T>().HasOne<User>().WithMany().HasForeignKey("ModifiedById").OnDelete(DeleteBehavior.Restrict);
        }

        public static void SetConcurrencyShadowProperty<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, IConcurrencyCheckEntity
        {
            builder.Entity<T>().Property<byte[]>("ConcurrencyStamp").IsRowVersion();
        }

        public static void SetDailyEntryShadowProperties<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, IRecordDailyEntry
        {
            // define shadow properties
            builder.Entity<T>().Property<Guid>("ClientId");
            builder.Entity<T>().Property<Guid>("BusinessId");
            builder.Entity<T>().Property<Guid>("UserId");
            builder.Entity<T>().Property<Guid?>("CounterId");
            builder.Entity<T>().Property<Guid?>("DayId");
        }

        public static void Seed(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = Guid.Parse("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                NameEnglish = "noble@gmail.com",
                Blocked = false,
                CompanyRegNo = "56ty60",
                CreatedDate = DateTime.UtcNow
            });
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                UserName = "noble@gmail.com",
                Email = "noble@gmail.com",
                NormalizedEmail = "noble@gmail.com",
                EmailConfirmed = true,
                NormalizedUserName = "NOBLE@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEEMJll7GgXWk1z2fatxJWFPeucssBaOZ+EXMY5MYkhRNow+oaTxB0nH+sWvTX6wKWA==",
                SecurityStamp = "HDM6TKME4T5DISZCHW3MHD6YLQFNSWC2",
                ConcurrencyStamp = "117c7248-5202-44d5-a7eb-8f2717eba7e9",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                CompanyId = Guid.Parse("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                FirstName = "Noble App",
                LastName = "",
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "C1448F88-49B4-476C-B07D-33514A0F9C1E",
                Name = "Noble Admin",
                NormalizedName = "NOBLE ADMIN",
                ConcurrencyStamp = "0590a33c-cd2d-4c93-9e17-fce19bc2bd9d",

            });
           
           
           
          
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "52E0A954-7039-483B-9224-99990743636D",
                Name = "Super Admin",
                NormalizedName = "SUPER ADMIN",
                ConcurrencyStamp = "0590a33c-cd2d-4c93-9e17-fce12bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "CEA36602-E3BD-4CF6-B186-5D8A3E558A04",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "0590a33c-cd2d-4d93-9e17-fce19bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "E5480E8E-A150-4C80-82AB-62B5A8EBFD1B",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "1590a33c-cd2d-4c93-9e17-fce19bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "f9d3868c-b58c-4c55-be2b-48d564bea081",
                Name = "Cashier",
                NormalizedName = "Cashier",
                ConcurrencyStamp = "1590a33c-cd2d-4c93-9e17-fce19bc2bd9d",

            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "C1448F88-49B4-476C-B07D-33514A0F9C1E",
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"
            });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "Email",
                ClaimValue = "noble@gmail.com"
            },
            new IdentityUserClaim<string>
            {
                Id = 2,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "CompanyId",
                ClaimValue = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"

            },
            new IdentityUserClaim<string>
            {
                Id = 3,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "Organization",
                ClaimValue = "Noble POS"

            },
            new IdentityUserClaim<string>
            {
                Id = 4,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "FullName",
                ClaimValue = "Noble App"

            }


            );
        }
    }
}

