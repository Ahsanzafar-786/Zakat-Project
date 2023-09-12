using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class CharityTransactionConfiguration : IEntityTypeConfiguration<CharityTransaction>
    {
        public void Configure(EntityTypeBuilder<CharityTransaction> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("decimal(18,4)");
            builder.HasOne(x => x.Beneficiaries)
                 .WithMany(x => x.CharityTransactions)
                 .HasForeignKey(x => x.BenificayId);
        }
    }
}
