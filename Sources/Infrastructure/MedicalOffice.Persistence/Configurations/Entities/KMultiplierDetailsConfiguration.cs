using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class KMultiplierDetailsConfiguration : BaseEntityTypeConfiguration<KMultiplierDetail, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<KMultiplierDetail> builder)
        {
            builder
                .HasOne(e => e.KMultiplier)
                .WithMany(e => e.KMultiplierDetails)
                .HasForeignKey(e => e.KMultiplierId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
