using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class AllergyConfiguration : BaseEntityTypeConfiguration<Allergy, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Allergy> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.Allergies)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.FDO)
                .WithMany(e => e.Allergies)
                .HasForeignKey(e => e.FDOId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.ICD11)
                .WithMany(e => e.Allergies)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
