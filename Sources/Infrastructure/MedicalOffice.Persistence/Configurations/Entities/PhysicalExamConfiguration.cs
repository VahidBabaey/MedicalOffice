using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PhysicalExamConfiguration : BaseEntityTypeConfiguration<PhysicalExam, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<PhysicalExam> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.PhysicalExams)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
