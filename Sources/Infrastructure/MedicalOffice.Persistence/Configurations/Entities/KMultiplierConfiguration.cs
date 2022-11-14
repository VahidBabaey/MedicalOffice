using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class KMultiplierConfiguration : BaseEntityTypeConfiguration<KMultiplier, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<KMultiplier> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.KMultipliers)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.KMultiplierDetails)
                .WithOne(e => e.KMultiplier)
                .HasForeignKey(e => e.KMultiplierId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
