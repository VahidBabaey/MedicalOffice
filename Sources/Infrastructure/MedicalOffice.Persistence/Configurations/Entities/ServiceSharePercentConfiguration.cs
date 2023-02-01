using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MedicalStaffServiceSharePercentConfiguration : BaseEntityTypeConfiguration<MedicalStaffServiceSharePercent, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MedicalStaffServiceSharePercent> builder)
        {
            builder
               .HasOne(e => e.Shift)
               .WithMany(e => e.MedicalStaffServiceSharePercents)
               .HasForeignKey(e => e.ShiftId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Service)
               .WithMany(e => e.MedicalStaffServiceSharePercents)
               .HasForeignKey(e => e.ServiceId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Section)
               .WithMany(e => e.MedicalStaffServiceSharePercents)
               .HasForeignKey(e => e.SectionId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
