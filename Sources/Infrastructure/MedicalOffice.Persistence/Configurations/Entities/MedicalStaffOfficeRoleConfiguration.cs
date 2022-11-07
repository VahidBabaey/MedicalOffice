using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MedicalStaffOfficeRoleConfiguration : BaseEntityTypeConfiguration<MedicalStaffOfficeRole, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MedicalStaffOfficeRole> builder)
        {
            builder
                .HasOne(userOfficeRole => userOfficeRole.Office)
                .WithMany(office => office.MedicalStaffOfficeRoles)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(userOfficeRole => userOfficeRole.Role)
                .WithMany(role => role.UserOfficeRoles)
                .HasForeignKey(userOfficeRole => userOfficeRole.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
