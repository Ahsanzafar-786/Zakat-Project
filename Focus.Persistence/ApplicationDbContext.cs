using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Focus.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder;

namespace Focus.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IUserHttpContextProvider _httpContextProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserHttpContextProvider httpContext) : base(options)
        {
            _httpContextProvider = httpContext;
        }
       
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Beneficiaries> Beneficiaries { get; set; }
        public virtual DbSet<AuthorizedPerson> AuthorizedPersons { get; set; }
        public virtual DbSet<BenificaryNote> BenificaryNotes { get; set; }
        public virtual DbSet<CharityResources> CharityResources { get; set; }
        public virtual DbSet<ApprovalPerson> ApprovalPersons { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Funds> Funds { get; set; }
        public virtual DbSet<BenificaryAuthorization> BenificaryAuthorizations { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<CharityTransaction> CharityTransaction { get; set; }
        public virtual DbSet<SelectedMonth> SelectedMonths { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ShadowProperties();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            SetGlobalQueryFilters(modelBuilder);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
        public override int SaveChanges()
        {
            ChangeTracker.SetShadowProperties(_httpContextProvider);
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.SetShadowProperties(_httpContextProvider);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public virtual void SetModified(object entity,string attribute,Guid value)
        {
            Entry(entity).Property(attribute).CurrentValue = value;
        }
        public virtual int SaveChangesAfter()
        {
            return base.SaveChanges();
        }
        private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;
                // set global filters
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    if (typeof(ITenantFilterableEntity).IsAssignableFrom(t))
                    {
                        // softdeletable and tenant (note do not filter just ITenant - too much filtering!
                        // just top level classes that have ITenantFilterableEntity
                        var method = SetGlobalQueryForSoftDeleteAndTenantMethodInfo.MakeGenericMethod(t);
                        method.Invoke(this, new object[] { modelBuilder });
                    }
                    else
                    {
                        // softdeletable
                        var method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
                        method.Invoke(this, new object[] { modelBuilder });
                    }
                }
                else if (typeof(ITenantFilterableEntity).IsAssignableFrom(t))
                {
                    // just to filter any entities with ITenantFilterableEntity
                    var method = SetGlobalQueryForTenantMethodInfo.MakeGenericMethod(t);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }
        private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDelete");
        private static readonly MethodInfo SetGlobalQueryForSoftDeleteAndTenantMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDeleteAndTenant");
        private static readonly MethodInfo SetGlobalQueryForTenantMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForTenant");
        public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));
        }
        public void SetGlobalQueryForSoftDeleteAndTenant<T>(ModelBuilder builder) where T : class, ISoftDelete, ITenant
        {
            builder.Entity<T>().HasQueryFilter(
                item => !EF.Property<bool>(item, "IsDeleted") &&
                        (DisableTenantFilter || EF.Property<Guid>(item, "CompanyId") == _httpContextProvider.GetCompanyId()));
        }
        public void SetGlobalQueryForTenant<T>(ModelBuilder builder) where T : class, ITenant
        {
            builder.Entity<T>().HasQueryFilter(
                item => (DisableTenantFilter || EF.Property<Guid>(item, "CompanyId") == _httpContextProvider.GetCompanyId()));
        }
        public bool DisableTenantFilter { get; set; }

    }
}
