
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PaymentAuthorizePersonItemConfiguration : IEntityTypeConfiguration<PaymentAuthorizePersonItem>
    {
        public void Configure(EntityTypeBuilder<PaymentAuthorizePersonItem> builder)
        {

            builder.HasOne(x => x.Beneficiaries)
                .WithMany(x => x.PaymentAuthorizePersonItems)
                .HasForeignKey(x => x.BenificayId);

            builder.HasOne(x => x.PaymentAuthorizePerson)
                .WithMany(x => x.PaymentAuthorizePersonItems)
                .HasForeignKey(x => x.PaymentAuthorizePersonId);


        }
    }
}
