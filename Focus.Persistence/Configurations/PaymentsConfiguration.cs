using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PaymentsConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("decimal(18,4)");

            builder.HasOne(x => x.Beneficiaries)
                  .WithMany(x => x.Payments)
                  .HasForeignKey(x => x.BenificayId);

            builder.HasOne(x => x.ApplicationUser)
                 .WithMany(x => x.Payments)
                 .HasForeignKey(x => x.UserId);


        }
    }
}
