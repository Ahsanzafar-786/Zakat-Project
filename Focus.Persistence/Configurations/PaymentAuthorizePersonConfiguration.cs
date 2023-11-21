using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Focus.Persistence.Configurations
{
    public class PaymentAuthorizePersonConfiguration : IEntityTypeConfiguration<PaymentAuthorizePerson>
    {
        public void Configure(EntityTypeBuilder<PaymentAuthorizePerson> builder)
        {

            builder.HasOne(x => x.AuthorizedPerson)
                .WithMany(x => x.PaymentAuthorizePersons)
                .HasForeignKey(x => x.AuthorizePersonId);

           


        }
    }
}
