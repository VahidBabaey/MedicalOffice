using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class CashPosConfiguration : BaseEntityTypeConfiguration<CashPos, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<CashPos> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.CashPoses)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
