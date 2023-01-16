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
            builder
               .HasOne(uor => uor.User)
               .WithMany(u => u.UserOfficeRoles)
               .HasForeignKey(uor => uor.UserId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(uor => uor.Office)
               .WithMany(o => o.UserOfficeRoles)
               .HasForeignKey(uor => uor.OfficeId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(uor => uor.Role)
               .WithMany(r => r.UserOfficeRoles)
               .HasForeignKey(uor => uor.RoleId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasQueryFilter(uor => uor.IsDeleted == false);
        }
    }
}
