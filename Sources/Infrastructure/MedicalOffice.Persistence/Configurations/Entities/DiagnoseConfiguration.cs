using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class DiagnoseConfiguration : BaseEntityTypeConfiguration<Diagnose, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Diagnose> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.Diagnoses)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.ICD11)
                .WithMany(e => e.Diagnoses)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
