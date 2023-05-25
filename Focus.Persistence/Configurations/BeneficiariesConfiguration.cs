using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Persistence.Configurations
{
    public class BeneficiariesConfiguration : IEntityTypeConfiguration<Beneficiaries>
    {
        public void Configure(EntityTypeBuilder<Beneficiaries> builder)
        {
            builder.Property(x => x.AmountPerMonth).HasColumnType("decimal(18,4)");
        }
    }
}
