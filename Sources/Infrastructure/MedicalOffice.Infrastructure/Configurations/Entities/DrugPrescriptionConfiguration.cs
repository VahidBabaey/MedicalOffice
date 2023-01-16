using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class DrugPrescriptionConfiguration : BaseEntityTypeConfiguration<DrugPrescription, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DrugPrescription> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.DrugPrescriptions)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.FDO)
                .WithMany(e => e.DrugPrescriptions)
                .HasForeignKey(e => e.FDOId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
