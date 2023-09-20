

using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("decimal(18,4)");

            builder.HasOne(x => x.ExpenseCategory)
                   .WithMany(x => x.Expenses)
                   .HasForeignKey(x => x.ExpenseCategoryId);
        }
    }
}
