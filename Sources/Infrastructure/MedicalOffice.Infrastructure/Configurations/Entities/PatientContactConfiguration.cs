using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PatientContactConfiguration : BaseEntityTypeConfiguration<PatientContact, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<PatientContact> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.PatientContacts)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
