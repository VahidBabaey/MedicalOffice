using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PatientTagConfiguration : BaseEntityTypeConfiguration<PatientTag, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<PatientTag> builder)
        {
            builder
                .HasQueryFilter(m => m.IsDeleted == false);
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.PatientTags)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
