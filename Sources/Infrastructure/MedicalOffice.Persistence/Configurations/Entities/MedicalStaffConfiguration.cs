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
                .HasMany(user => user.Receptions)
                .WithOne(e => e.MedicalStaff)
                .HasForeignKey(e => e.LoggedInUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(medicalStaff => medicalStaff.Appointments)
                .WithOne(e => e.MedicalStaff)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
