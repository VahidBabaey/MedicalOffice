using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class RoutineMedicationConfiguration : BaseEntityTypeConfiguration<RoutineMedication, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<RoutineMedication> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.RoutineMedications)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.FDO)
                .WithMany(e => e.RoutineMedications)
                .HasForeignKey(e => e.FDOId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
