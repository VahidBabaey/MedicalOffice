using MedicalOffice.Domain.Entities;
using MedicalOffice.Identity.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configuration
{
    public class UserOfficeRoleConfiguration : BaseEntityTypeConfiguration<UserOfficeRole, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserOfficeRole> builder)
        {
            //builder
            //    .HasOne(userOfficeRole => userOfficeRole.User)
            //    .WithMany(user => user.UserOfficeRoles)
            //    .HasForeignKey(userOfficeRole => userOfficeRole.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //builder
            //    .HasOne(userOfficeRole => userOfficeRole.Office)
            //    .WithMany(office => office.UserOfficeRoles)
            //    .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //builder
            //    .HasOne(userOfficeRole => userOfficeRole.Role)
            //    .WithMany(role => role.UserOfficeRoles)
            //    .HasForeignKey(userOfficeRole => userOfficeRole.RoleId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
