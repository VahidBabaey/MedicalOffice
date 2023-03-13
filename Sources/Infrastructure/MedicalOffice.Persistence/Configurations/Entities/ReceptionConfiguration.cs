using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ReceptionConfiguration : BaseEntityTypeConfiguration<Reception, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Reception> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Receptions)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Shift)
                .WithMany(e => e.Receptions)
                .HasForeignKey(e => e.ShiftId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.Receptions)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.ReceptionDetails)
                .WithOne(e => e.Reception)
                .HasForeignKey(e => e.ReceptionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
