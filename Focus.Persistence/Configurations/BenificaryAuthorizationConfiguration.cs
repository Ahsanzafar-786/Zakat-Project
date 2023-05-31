using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BenificaryAuthorizationConfiguration : IEntityTypeConfiguration<BenificaryAuthorization>
    {
        public void Configure(EntityTypeBuilder<BenificaryAuthorization> builder)
        {
            builder.HasOne(x => x.Beneficiaries)
                   .WithMany(x => x.BenificaryAuthorization)
                   .HasForeignKey(x => x.BenficaryId);

            builder.HasOne(x => x.AuthorizedPerson)
                  .WithMany(x => x.BenificaryAuthorization)
                  .HasForeignKey(x => x.AuthorizationPersonId);

            builder.HasOne(x => x.ApprovalPerson)
                  .WithMany(x => x.BenificaryAuthorization)
                  .HasForeignKey(x => x.ApprovalPersonId);
        }
    }
}
