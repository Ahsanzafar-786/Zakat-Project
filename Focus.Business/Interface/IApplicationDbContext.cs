using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Interface
{
    public interface IApplicationDbContext
    {
       
        DbSet<Company> Companies { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Beneficiaries> Beneficiaries { get; set; }
        DbSet<AuthorizedPerson> AuthorizedPersons { get; set; }
        DbSet<BenificaryNote> BenificaryNotes { get; set; }
        DbSet<CharityResources> CharityResources { get; set; }
        DbSet<ApprovalPerson> ApprovalPersons { get; set; }
        DbSet<PaymentType> PaymentTypes { get; set; }
        DbSet<Funds> Funds { get; set; }
        DbSet<BenificaryAuthorization> BenificaryAuthorizations { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Logs> Logs { get; set; }
        DbSet<CharityTransaction> CharityTransaction { get; set; }
        DbSet<SelectedMonth> SelectedMonths { get; set; }
        DbSet<Expense> Expenses { get; set; }
        DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        bool DisableTenantFilter { get; set; }
        int SaveChanges();
        int SaveChangesAfter();
        void SetModified(object entity, string attribute, Guid value);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
