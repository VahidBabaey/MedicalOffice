using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class GeneralExaminationConfiguration : BaseEntityTypeConfiguration<GeneralExamination, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<GeneralExamination> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.GeneralExaminations)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.ICD11)
                .WithMany(e => e.GeneralExaminations)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
