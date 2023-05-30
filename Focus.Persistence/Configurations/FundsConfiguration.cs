

using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class FundsConfiguration : IEntityTypeConfiguration<Funds>
    {
        public void Configure(EntityTypeBuilder<Funds> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("decimal(18,4)");
            builder.HasOne(x => x.CharityResources)
                   .WithMany(x => x.Funds)
                   .HasForeignKey(x => x.CharityResouceId);
        }
    }
}
