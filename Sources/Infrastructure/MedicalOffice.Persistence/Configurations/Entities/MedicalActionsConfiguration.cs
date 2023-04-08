using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MedicalActionsConfiguration : BaseEntityTypeConfiguration<MedicalAction, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MedicalAction> builder)
        {
            builder
                .HasQueryFilter(m => m.IsDeleted == false);
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.MedicalActions)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.RVU3)
                .WithMany(e => e.MedicalActions)
                .HasForeignKey(e => e.RVU3Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
