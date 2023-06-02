using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SelectedMonthConfiguration : IEntityTypeConfiguration<SelectedMonth>
    {
        public void Configure(EntityTypeBuilder<SelectedMonth> builder)
        {
            builder.HasOne(x => x.Payments)
                 .WithMany(x => x.SelectedMonth)
                 .HasForeignKey(x => x.PaymentId);
        }
    }
}
