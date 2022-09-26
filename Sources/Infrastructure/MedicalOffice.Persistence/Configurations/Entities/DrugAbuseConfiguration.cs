using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class DrugAbuseConfiguration : BaseEntityTypeConfiguration<DrugAbuse, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DrugAbuse> builder)
        {
            builder
                .HasOne(e => e.SNOMED)
                .WithMany(e => e.DrugAbuses)
                .HasForeignKey(e => e.SNOMEDId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.DrugAbuses)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
