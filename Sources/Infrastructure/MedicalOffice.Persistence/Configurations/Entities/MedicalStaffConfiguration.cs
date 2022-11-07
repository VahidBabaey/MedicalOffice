using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MedicalStaffConfiguration : BaseEntityTypeConfiguration<MedicalStaff, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MedicalStaff> builder)
        {
            builder
                .HasMany(m => m.Receptions)
                .WithOne(e => e.MedicalStaff)
                .HasForeignKey(e => e.LoggedInUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(m => m.Appointments)
                .WithOne(e => e.MedicalStaff)
                .HasForeignKey(e => e.MedicalStaffId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(m => m.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(m => m.SpecializationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
