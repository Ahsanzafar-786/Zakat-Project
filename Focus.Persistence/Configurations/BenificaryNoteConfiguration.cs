using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BenificaryNoteConfiguration : IEntityTypeConfiguration<BenificaryNote>
    {
        public void Configure(EntityTypeBuilder<BenificaryNote> builder)
        {
            builder.HasOne(x => x.Beneficiaries)
                    .WithMany(x => x.BenificaryNotes)
                    .HasForeignKey(x => x.BenificaryId);
        }
    }
}
