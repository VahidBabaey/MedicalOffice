using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PatientAddressConfiguration : BaseEntityTypeConfiguration<PatientAddress, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<PatientAddress> builder)
        {
            builder
                .HasQueryFilter(m => m.IsDeleted == false);
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.PatientAddresses)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
