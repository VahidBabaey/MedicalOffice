using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MedicalStaffOfficeSpecializationConfiguration : BaseEntityTypeConfiguration<UserOfficeRole, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserOfficeRole> builder)
        {
            //builder
            //   .HasOne(e => e.MedicalStaff)
            //   .WithMany(e => e.UserOfficeRoles)
            //   .HasForeignKey(e => e.MedicalStaffId)
            //   .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Office)
               .WithMany(e => e.UserOfficeRoles)
               .HasForeignKey(e => e.OfficeId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Role)
               .WithMany(e => e.UserOfficeRoles)
               .HasForeignKey(e => e.RoleId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
