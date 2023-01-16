using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class FormCommitmentsConfiguration : BaseEntityTypeConfiguration<FormCommitment, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<FormCommitment> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.FormCommitments)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
